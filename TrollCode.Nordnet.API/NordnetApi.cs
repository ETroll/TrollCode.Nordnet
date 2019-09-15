using System;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using TrollCode.Nordnet.API.Responses;

using System.Net.Http.Headers;
using System.Threading;

namespace TrollCode.Nordnet.API
{
    public class NordnetApi : IDisposable
    {
        private readonly RSACryptoServiceProvider publicKey;
        private readonly HttpClient client;
        Timer keepAliveTimer;

        public NordnetApi(RSACryptoServiceProvider key, string baseAddress)
        {
            publicKey = key;
            client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            ServiceName = "NEXTAPI";
            CurrentSession = null;
        }

        public string ServiceName { get; set; }
        public NordnetSession CurrentSession { get; private set; }

        public event EventHandler OnSessionDisconnected;

        public static RSACryptoServiceProvider GetCryptoserviceProviderForParameterXML(string xmlstr)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            RSAParameters parameters = Activator.CreateInstance<RSAParameters>();

            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(xmlstr)))
            {
                XmlSerializer serializer = new XmlSerializer(parameters.GetType());
                parameters = (RSAParameters)serializer.Deserialize(ms);
            }

            provider.ImportParameters(parameters);

            return provider;
        }
        
        public async Task Login(string username, string password)
        {
            string timestamp = Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds)
                .ToString();

            string encoded = $"{Convert.ToBase64String(Encoding.UTF8.GetBytes(username))}:" +
                             $"{Convert.ToBase64String(Encoding.UTF8.GetBytes(password))}:" +
                             $"{Convert.ToBase64String(Encoding.UTF8.GetBytes(timestamp))}";

            string auth = Convert.ToBase64String(publicKey.Encrypt(Encoding.UTF8.GetBytes(encoded), false));

            using (HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"service", ServiceName},
                { "auth", auth}
            }))
            {
                HttpResponseMessage loginResult = await client.PostAsync($"next/2/login", content);

                if (loginResult.IsSuccessStatusCode)
                {
                    try
                    {
                        LoginResponse session = JsonConvert.DeserializeObject<LoginResponse>(await loginResult.Content.ReadAsStringAsync());

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            "Basic",
                            Convert.ToBase64String(Encoding.ASCII.GetBytes($"{session.Session_key}:{session.Session_key}"))
                        );

                        CurrentSession = new NordnetSession
                        {
                            PrivateFeed = session.Private_feed,
                            PublicFeed = session.Public_feed,
                            Country = session.Country,
                            Environment = session.Environment,
                            SessionId = session.Session_key
                        };

                        int interval = (session.Expires_in - 30) * 1000;
                        keepAliveTimer = new Timer(KeepAliveTick, null, interval, interval);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Could not deserialize response from Nordnet");
                    }
                }
                else
                {
                    throw new Exception("Login was not successfull", new Exception(loginResult.ReasonPhrase));
                }
            };
        }

        private async void KeepAliveTick(object state)
        {
            LoggedInStatus status = await ParseResponse<LoggedInStatus>(await client.PutAsync("next/2/login", new StringContent(string.Empty)));
            if(!status.Logged_in)
            {
                OnSessionDisconnected?.Invoke(this, null);
                keepAliveTimer.Dispose();
            }
        }

        private async Task<T> GetData<T>(string uri)
        {
            return await ParseResponse<T>(await client.GetAsync(uri));
        }

        private async Task<T> ParseResponse<T>(HttpResponseMessage response)
        {
            string data = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(data);
                }
                catch (Exception)
                {
                    throw new Exception("Could not deserialize response from Nordnet");
                }
            }
            throw new Exception($"Call to {response.RequestMessage.RequestUri} was not successfull", new Exception(response.ReasonPhrase));
        }

        
        public async Task QueryIntruments(string query)
        {
            HttpResponseMessage response = await client.GetAsync($"/next/2/instruments?query={query}");
            string data = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Request was not successful. HTTP result: {response.StatusCode} {response.ReasonPhrase}");
            }
            Console.WriteLine(data);

            throw new NotImplementedException();
        }

        public async Task<List<IntrumentList>> GetIntrumentLists()
        {
            return await GetData<List<IntrumentList>>("/next/2/lists");
        }

        public async Task<List<Market>> GetMarkets()
        {
            return await GetData<List<Market>>("/next/2/markets");
        }


        public void Dispose()
        {
            Console.WriteLine("Disposing Nordnet API");
            keepAliveTimer?.Dispose();
            client?.Dispose();
        }
    }
}

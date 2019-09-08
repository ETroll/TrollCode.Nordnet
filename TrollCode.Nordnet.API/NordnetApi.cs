using System;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using TrollCode.Nordnet.API.Responses;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;
using TrollCode.Nordnet.API.FeedModels;
using System.Net.Http.Headers;

namespace TrollCode.Nordnet.API
{
    public class NordnetApi
    {

        RSACryptoServiceProvider publicKey;
        readonly HttpClient client;

        public NordnetApi(RSACryptoServiceProvider key, string baseAddress)
        {
            publicKey = key;
            client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            ServiceName = "NEXTAPI";
        }

        public string ServiceName { get; set; }

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
        
        public async Task<LoginResponse> Login(string username, string password)
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

                if(loginResult.IsSuccessStatusCode)
                {
                    try
                    {
                        LoginResponse session = JsonConvert.DeserializeObject<LoginResponse>(await loginResult.Content.ReadAsStringAsync());

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            "Basic",
                            Convert.ToBase64String(Encoding.ASCII.GetBytes($"{session.Session_key}:{session.Session_key}"))
                        );

                        return session;
                    }
                    catch(Exception)
                    {
                        throw new Exception("Could not deserialize response from Nordnet");
                    }
                }
                throw new Exception("Login was not successfull", new Exception(loginResult.ReasonPhrase));
            };

        }

        //GET 

        public async Task QueryIntruments()
        {
            HttpResponseMessage response = await client.GetAsync($"/next/2/instruments?query=");

            string data = await response.Content.ReadAsStringAsync();

            //try
            //{
            //    return JsonConvert.DeserializeObject<List<IntrumentListReponse>>(data);
            //}
            //catch (Exception)
            //{
            //    throw new Exception("Could not deserialize response from Nordnet");
            //}
            //throw new Exception("GetMarkets was not successfull", new Exception(response.ReasonPhrase));
        }

        public async Task<List<IntrumentListReponse>> GetIntrumentLists()
        {
            HttpResponseMessage response = await client.GetAsync("/next/2/lists");

            string data = await response.Content.ReadAsStringAsync();

            try
            {
                return JsonConvert.DeserializeObject<List<IntrumentListReponse>>(data);
            }
            catch (Exception)
            {
                throw new Exception("Could not deserialize response from Nordnet");
            }
            throw new Exception("GetMarkets was not successfull", new Exception(response.ReasonPhrase));
        }

        public async Task<List<MarketResponse>> GetMarkets()
        {
            HttpResponseMessage response = await client.GetAsync("/next/2/markets");

            string data = await response.Content.ReadAsStringAsync();

            try
            {
                return JsonConvert.DeserializeObject<List<MarketResponse>>(data);
            }
            catch (Exception)
            {
                throw new Exception("Could not deserialize response from Nordnet");
            }
            throw new Exception("GetMarkets was not successfull", new Exception(response.ReasonPhrase));
        }



        public void ConnectToFeed(FeedInformation information, string sessionid)
        {
            TcpClient client = new TcpClient(information.Hostname, information.Port);
            Console.WriteLine("Client connected.");
            

            SslStream sslStream = new SslStream(client.GetStream(),false, new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
            {
                //Dont validate for now. Add later!
                return true;

            }), null);


            try
            {
                sslStream.AuthenticateAsClient("I Will Fail Now");
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                Console.WriteLine("Authentication failed - closing the connection.");
                client.Close();
                return;
            }

            sslStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new Command
            {
                cmd = "login",
                args = new Dictionary<string, string>
                {
                    {"session_key", sessionid}
                }
            }) + "\n"));
            sslStream.Flush();

            sslStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new Command
            {
                //{"market_id":14,"country":"DK","name":"Nasdaq OMX Copenhagen"},
                cmd = "subscribe", //"args":{"t":"price", "i":"1869", "m":30}}
                args = new Dictionary<string, string>
                {
                    {"t", "price"},
                    {"i", "1869" },
                    {"m", "14"}
                }
            }) + "\n"));
            sslStream.Flush();

            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                bytes = sslStream.Read(buffer, 0, buffer.Length);

                Decoder decoder = Encoding.ASCII.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);

                Console.WriteLine($"{DateTime.UtcNow.ToString("mm:ss")}: Got {bytes} bytes - {new string(chars)}");

                try
                {
                    FeedResponse response = JsonConvert.DeserializeObject<FeedResponse>(new string(chars));


                }
                catch(Exception ex)
                {
                    Console.WriteLine("Could not parse feed response");
                    Console.WriteLine(ex.Message);
                }
            } while (bytes != 0);

            Console.WriteLine($"The server has disconnected - Stopping");

        }
    }
}

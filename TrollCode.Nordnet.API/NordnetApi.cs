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
                        return JsonConvert.DeserializeObject<LoginResponse>(await loginResult.Content.ReadAsStringAsync());
                    }
                    catch(Exception)
                    {
                        throw new Exception("Could not deserialize response from Nordnet");
                    }
                }
                throw new Exception("Login was not successfull", new Exception(loginResult.ReasonPhrase));
            };

        }
    }
}

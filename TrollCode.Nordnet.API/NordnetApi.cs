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
using System.Linq;

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

                        long interval = (session.Expires_in - 30) * 1000;
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
                catch (Exception ex)
                {
                    throw new Exception($"Could not deserialize response from Nordnet: {ex.Message}");
                }
            }
            throw new Exception($"Call to {response.RequestMessage.RequestUri} was not successfull", new Exception(response.ReasonPhrase));
        }

        


        public async Task<List<IntrumentList>> GetIntrumentLists()
        {
            return await GetData<List<IntrumentList>>("/next/2/lists");
        }

        public async Task<List<Instrument>> GetInstrumentsInList(long listId)
        {
            return await GetData<List<Instrument>>($"/next/2/lists/{listId}");
        }

        public async Task<List<Market>> GetMarkets()
        {
            return await GetData<List<Market>>("/next/2/markets");
        }

        #region System Queries
        public async Task<SystemStatus> GetSystemStatus()
        {
            return await GetData<SystemStatus>("/next/2");
        }
        #endregion

        #region Account Queries (With Order)
        public async Task<List<Account>> GetAccounts()
        {
            return await GetData<List<Account>>("/next/2/accounts");
        }

        public async Task<AccountInfo> GetAccountInfo(long accountno)
        {
            return await GetData<AccountInfo>($"/next/2/accounts/{accountno}");
        }

        public async Task<LedgerInformation> GetLedgerInformationForAccount(long accountno)
        {
            return await GetData<LedgerInformation>($"/next/2/accounts/{accountno}/ledgers");
        }

        public async Task<List<Order>> GetOrdersForAccount(long accountno, bool includeDeleted = false)
        {
            return await GetData<List<Order>>($"/next/2/accounts/{accountno}/orders{(includeDeleted ? "?deleted=true" : string.Empty)}");
        }

        public async Task<OrderReply> PostOrder(long accountno, SendOrder order)
        {
            using (HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"identifier", "1501855"},
                {"market_id", "30"},
                {"volume", "10"},
                {"side", "BUY"},
                {"price", "2" },
                {"currency", "SEK" }
            }))
            {
                HttpResponseMessage orderResult = await client.PostAsync($"/next/2/accounts/{accountno}/orders", content);

                return await ParseResponse<OrderReply>(orderResult);
            };
        }

        public async Task<OrderReply> ActivateOrder(long accountno, long orderid)
        {
            HttpResponseMessage orderResult = await client.PutAsync($"/next/2/accounts/{accountno}/orders/{orderid}/activate", new StringContent(string.Empty));
            return await ParseResponse<OrderReply>(orderResult);
        }

        /// <summary>
        /// Updates a order with new volume and/or price.
        /// If no updated price or volume is set, then this method does NOTHING!
        /// </summary>
        /// <param name="accountno">The account number</param>
        /// <param name="orderid">The order id</param>
        /// <param name="newVolume">(optional) Updated volume</param>
        /// <param name="newPrice">(optional) Updated price</param>
        /// <param name="newCurrency">Required if a new price is set</param>
        /// <returns>The order reply or null if no updated data was set</returns>
        public async Task<OrderReply> UpdateOrder(long accountno, long orderid, long newVolume = 0, decimal newPrice = 0, string newCurrency = null)
        {
            string queryParameters = string.Empty;
            if(newPrice > 0)
            {
                if(string.IsNullOrWhiteSpace(newCurrency))
                {
                    //TODO: Validate against known currencies?
                    throw new ArgumentException("Currency can not be empty if a new price is set");
                }
                queryParameters += $"price={newPrice}&currency={newCurrency}";
            }

            if (newVolume > 0)
            {
                queryParameters += $"{(string.IsNullOrWhiteSpace(queryParameters) ? string.Empty : "&")}volume={newVolume}";
            }

            if(!string.IsNullOrWhiteSpace(queryParameters))
            {
                HttpResponseMessage orderResult = await client.PutAsync($"/next/2/accounts/{accountno}/orders/{orderid}?{queryParameters}", new StringContent(string.Empty));
                return await ParseResponse<OrderReply>(orderResult);
            }
            return null;
        }

        public async Task<OrderReply> DeleteOrder(long accountno, long orderid)
        {
            HttpResponseMessage orderResult = await client.DeleteAsync($"/next/2/accounts/{accountno}/orders/{orderid}");
            return await ParseResponse<OrderReply>(orderResult);
        }

        public async Task<List<Position>> GetPositionsForAccount(long accountno)
        {
            return await GetData<List<Position>>($"/next/2/accounts/{accountno}/positions");
        }

        public async Task<List<Trade>> GetTradesForAccount(long accountno)
        {
            return await GetData<List<Trade>>($"/next/2/accounts/{accountno}/trades");
        }
        #endregion

        #region Country Queries
        public async Task<List<CountryInformation>> GetCountries()
        {
            return await GetData<List<CountryInformation>>("/next/2/countries");
        }
        public async Task<List<CountryInformation>> GetInformationForCountryCodes(string[] countryCodes)
        {
            return await GetData<List<CountryInformation>>($"/next/2/countries/{string.Join(",",countryCodes)}");
        }
        public async Task<CountryInformation> GetInformationForCountryCode(string countryCode)
        {
            return await GetData<CountryInformation>($"/next/2/countries/{countryCode}");
        }
        #endregion

        #region Indicator Queries
        public async Task<List<Indicator>> GetIndicators()
        {
            return await GetData<List<Indicator>>("/next/2/indicators");
        }
        /// <summary>
        /// Returns full information for indicators if found
        /// </summary>
        /// <param name="indicators">Array of indicator [src]s and [identifier]s</param>
        /// <returns>Indicator information</returns>
        public async Task<List<Indicator>> GetInformationForIndicators(Tuple<string, string>[] indicators)
        {
            return await GetData<List<Indicator>>($"/next/2/indicators/{string.Join(",", indicators.Select(x => $"{x.Item1}:{x.Item2}"))}");
        }
        /// <summary>
        /// Returns full information for indicator if found
        /// </summary>
        /// <param name="indicator">The indicator [src] and [identifier]</param>
        /// <returns>Indicator information</returns>
        public async Task<Indicator> GetInformationForIndicator(Tuple<string, string> indicator)
        {
            return await GetData<Indicator>($"/next/2/indicators/{indicator.Item1}:{indicator.Item2}");
        }
        #endregion

        #region Instrument Queries
        public async Task<List<Instrument>> SearchForIntruments(string query)
        {
            throw new NotImplementedException();
            //HttpResponseMessage response = await client.GetAsync($"/next/2/instruments?query={query}");
            //string data = await response.Content.ReadAsStringAsync();

            //if (!response.IsSuccessStatusCode)
            //{
            //    Console.WriteLine($"Request was not successful. HTTP result: {response.StatusCode} {response.ReasonPhrase}");
            //}
            //Console.WriteLine(data);
        }

        public async Task<List<Instrument>> GetInstrumentsForIds(long[] instrumentIds)
        {
            return await GetData<List<Instrument>>($"/next/2/instruments/{string.Join(",", instrumentIds.Select(x => x.ToString()))}");
        }
        public async Task<Instrument> GetInstrumentForId(long instrumentId)
        {
            return await GetData<Instrument>($"/next/2/instruments/{instrumentId}");
        }

        public async Task<List<Instrument>> GetLeveratesForInstrument(long instrumentId, )
        {
            List<Tuple<string,string>> queryParameters = new List<Tuple<string, string>>();

            //queryParameters.

            return await GetData<List<Instrument>>($"/next/2/instruments/{instrumentId}/leverages{(queryParameters.Count() > 0 ? "?" + string.Join(",", queryParameters.Select(x => $"{x.Item1}={x.Item2}")) : string.Empty)}");
        }


        #endregion

        public void Dispose()
        {
            Dispose(true);
            //Just in case an inherited class adds a finalizer
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            Console.WriteLine("Disposing Nordnet API");
            if(disposing)
            {
                keepAliveTimer?.Dispose();
                client?.Dispose();
            }
        }
    }
}

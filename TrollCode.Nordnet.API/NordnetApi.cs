using System;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Trollcode.Nordnet.API.Responses;

using System.Net.Http.Headers;
using System.Threading;
using System.Linq;
using System.Web;

namespace Trollcode.Nordnet.API
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

        /// <summary>
        /// Logs in to a Nordnet Session and returns a NordnetSession object with feeds, environment and sessionid.
        /// Throws Exception if login is not successfull
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Nordnet Session object with session values</returns>
        public async Task<NordnetSession> LoginAsync(string username, string password)
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

                        return CurrentSession;
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
            if (!status.Logged_in)
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
            if (newPrice > 0)
            {
                if (string.IsNullOrWhiteSpace(newCurrency))
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

            if (!string.IsNullOrWhiteSpace(queryParameters))
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
            return await GetData<List<CountryInformation>>($"/next/2/countries/{HttpUtility.UrlEncode(string.Join(",", countryCodes))}");
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
            return await GetData<List<Indicator>>($"/next/2/indicators/{HttpUtility.UrlEncode(string.Join(",", indicators.Select(x => $"{x.Item1}:{x.Item2}")))}");
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
        public Task<List<Instrument>> SearchForIntruments(string query)
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
            return await GetData<List<Instrument>>($"/next/2/instruments/{HttpUtility.UrlEncode(string.Join(",", instrumentIds.Select(x => x.ToString())))}");
        }
        public async Task<Instrument> GetInstrumentForId(long instrumentId)
        {
            return await GetData<Instrument>($"/next/2/instruments/{instrumentId}");
        }

        public async Task<List<Instrument>> GetLeveragesForInstrument(long instrumentId, DateTime? expirationDate = null, long issuerId = 0, char? marketView = null, string instrumentType = null, string instrumentGroupType = null, string currency = null)
        {
            //TODO: Check out market view, this can be a enum instead of char D or U. (See docs)
            List<Tuple<string, string>> queryParameters = new List<Tuple<string, string>>();

            if (expirationDate.HasValue)
            {
                //Format: 2014-07-08
                queryParameters.Add(new Tuple<string, string>("expiration_date", expirationDate.Value.ToString("yyyy-MM-dd")));
            }

            if (issuerId > 0)
            {
                queryParameters.Add(new Tuple<string, string>("issuer_Id", issuerId.ToString()));
            }

            if (marketView.HasValue)
            {
                queryParameters.Add(new Tuple<string, string>("market_view", marketView.ToString()));
            }

            if (!string.IsNullOrWhiteSpace(instrumentType))
            {
                queryParameters.Add(new Tuple<string, string>("instrument_type", instrumentType));
            }

            if (!string.IsNullOrWhiteSpace(instrumentGroupType))
            {
                queryParameters.Add(new Tuple<string, string>("instrument_group_type", instrumentGroupType));
            }

            if (!string.IsNullOrWhiteSpace(currency))
            {
                queryParameters.Add(new Tuple<string, string>("currency", currency));
            }

            return await GetData<List<Instrument>>($"/next/2/instruments/{instrumentId}/leverages{(queryParameters.Count() > 0 ? "?" + string.Join("&", queryParameters.Select(x => $"{x.Item1}={x.Item2}")) : string.Empty)}");
        }

        public async Task<List<LeverageFilter>> GetLeverageFiltersForInstrument(long instrumentId, DateTime? expirationDate = null, long issuerId = 0, char? marketView = null, string instrumentType = null, string instrumentGroupType = null, string currency = null)
        {
            List<Tuple<string, string>> queryParameters = new List<Tuple<string, string>>();

            if (expirationDate.HasValue)
            {
                queryParameters.Add(new Tuple<string, string>("expiration_date", expirationDate.Value.ToString("yyyy-MM-dd")));
            }

            if (issuerId > 0)
            {
                queryParameters.Add(new Tuple<string, string>("issuer_Id", issuerId.ToString()));
            }

            if (marketView.HasValue)
            {
                queryParameters.Add(new Tuple<string, string>("market_view", marketView.ToString()));
            }

            if (!string.IsNullOrWhiteSpace(instrumentType))
            {
                queryParameters.Add(new Tuple<string, string>("instrument_type", instrumentType));
            }

            if (!string.IsNullOrWhiteSpace(instrumentGroupType))
            {
                queryParameters.Add(new Tuple<string, string>("instrument_group_type", instrumentGroupType));
            }

            if (!string.IsNullOrWhiteSpace(currency))
            {
                queryParameters.Add(new Tuple<string, string>("currency", currency));
            }

            return await GetData<List<LeverageFilter>>($"/next/2/instruments/{instrumentId}/leverages/filters{(queryParameters.Count() > 0 ? "?" + string.Join("&", queryParameters.Select(x => $"{x.Item1}={x.Item2}")) : string.Empty)}");
        }

        /// <summary>
        /// Returns a list of call/put option pairs. They are balanced on strike price. In order to find underlyings with options use "Get underlyings". To get available expiration dates use "Get option pair filters"
        /// </summary>
        /// <param name="instrumentId">The instrument id of the underlying instrument	</param>
        /// <param name="expirationDate">Show only option pairs instruments with a specific expiration date	</param>
        /// <returns>A list of call/put option pairs</returns>
        public async Task<List<OptionPair>> GetOptionPairsForInstrument(long instrumentId, DateTime? expirationDate = null)
        {
            return await GetData<List<OptionPair>>($"/next/2/instruments/{instrumentId}/option_pairs{(expirationDate.HasValue ? "?expiration_date=" + expirationDate.Value.ToString("yyyy-MM-dd") : string.Empty)}");
        }

        /// <summary>
        /// Returns valid filter values. Can be used to fill comboboxes in clients to filter options pair results. The same filters can be applied on this request to exclude invalid filter combinations
        /// </summary>
        /// <param name="instrumentId">The instrument id of the underlying instrument</param>
        /// <param name="expirationDate">Show only leverage instruments with a specific expiration date	</param>
        /// <returns>A optionfilter containing a list of valid expiration dates</returns>
        public async Task<OptionPairFilter> GetOptionPairFiltersForInstrument(long instrumentId, DateTime? expirationDate = null)
        {
            return await GetData<OptionPairFilter>($"/next/2/instruments/{instrumentId}/option_pairs/filters{(expirationDate.HasValue ? "?expiration_date=" + expirationDate.Value.ToString("yyyy-MM-dd") : string.Empty)}");
        }

        /// <summary>
        /// Lookup specfic instrument with prededfined fields. Please note that this is not a search, only exact matches is returned.
        /// </summary>
        /// <param name="marketId">The marked to lookup the instrument in</param>
        /// <param name="idenifier">The identifier for the instrument to look up</param>
        /// <returns>A instrument or null if nothing was found</returns>
        public async Task<Instrument> LookupUpInstrument(long marketId, long idenifier)
        {
            if (idenifier <= 0 || marketId <= 0)
            {
                throw new ArgumentException("All paramters have to contain a value larger than zero");
            }
            return await GetData<Instrument>($"/next/2/instruments/lookup/market_id_identifier/{marketId}:{idenifier}");
        }

        /// <summary>
        /// Lookup specfic instrument with prededfined fields. Please note that this is not a search, only exact matches is returned.
        /// </summary>
        /// <param name="isin">The instrument ISIN</param>
        /// <param name="currency">The instrument Currency</param>
        /// <param name="marketId">The marked to lookup the instrument in</param>
        /// <returns>A instrument or null if nothing was found</returns>
        public async Task<Instrument> LookupUpInstrumentByISIN(string isin, string currency, long marketId)
        {
            if (string.IsNullOrWhiteSpace(isin) || string.IsNullOrWhiteSpace(currency) || marketId <= 0)
            {
                throw new ArgumentException("All paramters have to contain a value");
            }
            return await GetData<Instrument>($"/next/2/instruments/lookup/isin_code_currency_market_id/{isin}:{currency}:{marketId}");
        }

        /// <summary>
        /// Get all instrument sectors
        /// </summary>
        /// <param name="group">Filter result on specified groups</param>
        /// <returns>A list of the available sectors</returns>
        public async Task<List<SectorType>> GetAllInstrumentSectors(string group = null)
        {
            return await GetData<List<SectorType>>($"/next/2/instruments/sectors{(!string.IsNullOrEmpty(group) ? "?group=" + group : string.Empty)}");
        }

        /// <summary>
        /// Get one or more sector
        /// </summary>
        /// <param name="sectors">Array of sectors you want information for</param>
        /// <returns>A list of sectors with information</returns>
        public async Task<List<SectorType>> GetSectorInformation(string[] sectors)
        {
            return await GetData<List<SectorType>>($"/next/2/instruments/sectors/{HttpUtility.UrlEncode(string.Join(",", sectors))}");
        }

        /// <summary>
        /// Get all instrument types. Please note that these types is used for both instrument_type and instrument_group_type.
        /// </summary>
        /// <returns>A list of all instrument types available</returns>
        public async Task<List<InstrumentType>> GetAllInstrumentTypes()
        {
            return await GetData<List<InstrumentType>>($"/next/2/instruments/types");
        }

        /// <summary>
        /// Get info of one orde more instrument type.
        /// </summary>
        /// <param name="instrumentTypes">The instrument types to get information on</param>
        /// <returns>A list of all instrument types</returns>
        public async Task<List<InstrumentType>> GetInstrumentTypes(string[] instrumentTypes)
        {
            return await GetData<List<InstrumentType>>($"/next/2/instruments/types/{HttpUtility.UrlEncode(string.Join(",", instrumentTypes))}");
        }

        /// <summary>
        /// Get instruments that are underlyings for a specific type of instruments. The query can return instrument that have option derivatives or leverage derivatives. Warrants are included in the leverage derivatives
        /// </summary>
        /// <param name="currency">The currency of the derivative. Please note that the underlying can have a different currency	</param>
        /// <returns></returns>
        public async Task<List<Instrument>> GetUnderlyingInstrumentsForLeverageDerivative(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new ArgumentException("A currency is needed");
            }

            return await GetData<List<Instrument>>($"/next/2/instruments/underlyings/leverage/{currency}");
        }

        /// <summary>
        /// Get instruments that are underlyings for a specific type of instruments. The query can return instrument that have option derivatives or leverage derivatives. Warrants are included in the leverage derivatives
        /// </summary>
        /// <param name="currency">The currency of the derivative. Please note that the underlying can have a different currency	</param>
        /// <returns></returns>
        public async Task<List<Instrument>> GetUnderlyingInstrumentsForOptionPairDerivative(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new ArgumentException("A currency is needed");
            }

            return await GetData<List<Instrument>>($"/next/2/instruments/underlyings/option_pair/{currency}");
        }

        #endregion

        #region Instrument List Queries
        public async Task<List<IntrumentList>> GetIntrumentLists()
        {
            return await GetData<List<IntrumentList>>("/next/2/lists");
        }

        public async Task<List<Instrument>> GetInstrumentsInList(long listId)
        {
            if (listId <= 0)
            {
                throw new ArgumentException("A valid listId with value of 0 or more is required");
            }

            return await GetData<List<Instrument>>($"/next/2/lists/{listId}");
        }
        #endregion

        #region Market Queries

        public async Task<List<Market>> GetAllMarkets()
        {
            return await GetData<List<Market>>("/next/2/markets");
        }

        public async Task<List<Market>> GetMarkets(long[] marketIds)
        {
            return await GetData<List<Market>>($"/next/2/markets/{HttpUtility.UrlEncode(string.Join(",", marketIds.Select(x => x.ToString())))}");
        }

        public async Task<List<RealtimeAccess>> GetMarketRealtimeAccessList()
        {
            return await GetData<List<RealtimeAccess>>("/next/2/realtime_access");
        }

        #endregion

        #region News Queries

        public async Task<List<NewsPreview>> GetNews(string query = null, long[] instrumentIds = null, int days = 0, string[] newsLangs = null, string[] newsCountries = null, long[] marketIds = null, int limit = 100, int offset = 0, long[] sourceIds = null)
        {
            List<Tuple<string, string>> queryParameters = new List<Tuple<string, string>>();

            if (!string.IsNullOrWhiteSpace(query))
            {
                queryParameters.Add(new Tuple<string, string>("query", query));
            }
            if (instrumentIds != null && instrumentIds.Length > 0)
            {
                queryParameters.Add(new Tuple<string, string>("instrument_id", string.Join(",", instrumentIds.Select(x => x.ToString()))));
            }
            if (days > 0)
            {
                queryParameters.Add(new Tuple<string, string>("days", days.ToString()));
            }
            if (newsLangs != null && newsLangs.Length > 0)
            {
                queryParameters.Add(new Tuple<string, string>("news_lang", string.Join(",", newsLangs)));
            }
            if (newsCountries != null && newsCountries.Length > 0)
            {
                queryParameters.Add(new Tuple<string, string>("news_country", string.Join(",", newsCountries)));
            }
            if (marketIds != null && marketIds.Length > 0)
            {
                queryParameters.Add(new Tuple<string, string>("market_id", string.Join(",", marketIds.Select(x => x.ToString()))));
            }
            if (limit > 0)
            {
                if (limit > 100)
                {
                    throw new ArgumentException("Limit can not be larger than the max value of 100");
                }
                queryParameters.Add(new Tuple<string, string>("limit", limit.ToString()));
            }

            if (offset >= 0)
            {
                queryParameters.Add(new Tuple<string, string>("offset", offset.ToString()));
            }

            if (sourceIds != null && sourceIds.Length > 0)
            {
                queryParameters.Add(new Tuple<string, string>("source_id", string.Join(",", sourceIds.Select(x => x.ToString()))));
            }

            return await GetData<List<NewsPreview>>($"/next/2/news{(queryParameters.Count() > 0 ? "?" + string.Join("&", queryParameters.Select(x => $"{x.Item1}={HttpUtility.UrlEncode(x.Item2)}")) : string.Empty)}");
        }

        public async Task<NewsItem> GetNewsItem(long itemId)
        {
            return (await GetNewsItems(new long[] { itemId })).FirstOrDefault();
        }

        public async Task<List<NewsItem>> GetNewsItems(long[] itemIds)
        {
            return await GetData<List<NewsItem>>($"/next/2/news/{HttpUtility.UrlEncode(string.Join(",", itemIds.Select(x => x.ToString())))}");
        }

        public async Task<List<NewsSource>> GetNewsSources()
        {
            return await GetData<List<NewsSource>>("/next/2/news_sources");
        }

        #endregion

        #region Tick Sizes
        public async Task<List<TicksizeTable>> GetAllTicksizeTables()
        {
            return await GetData<List<TicksizeTable>>("/next/2/tick_sizes");
        }

        public async Task<Instrument> GetTicksizeTable(long tableId)
        {
            return await GetData<Instrument>($"/next/2/tick_sizes/{tableId}");
        }
        #endregion

        #region Trades
        public Task<List<TradingInformation>> GetTradingInformationForTradable(Tradable tradable)
        {
            return GetTradingInformationForTradables(new List<Tradable> { tradable });
        }

        public async Task<List<TradingInformation>> GetTradingInformationForTradables(List<Tradable> tradables)
        {
            return await GetData<List<TradingInformation>>($"/next/2/tradables/info/{HttpUtility.UrlEncode(string.Join(",", tradables.Select(x => x.Market_id + ":" + x.Identifier)))}");
        }

        /// <summary>
        /// Can be used for populating instrument price graphs for today. Resolution is one minute
        /// </summary>
        /// <param name="tradable"></param>
        /// <returns></returns>
        public Task<List<IntradayTrades>> GetIntradayTradesForTradable(Tradable tradable)
        {
            return GetIntradayTradesForTradables(new List<Tradable> { tradable });
        }

        /// <summary>
        /// Can be used for populating instrument price graphs for today. Resolution is one minute
        /// </summary>
        /// <param name="tradables"></param>
        /// <returns></returns>
        public async Task<List<IntradayTrades>> GetIntradayTradesForTradables(List<Tradable> tradables)
        {
            return await GetData<List<IntradayTrades>>($"/next/2/tradables/intraday/{HttpUtility.UrlEncode(string.Join(",", tradables.Select(x => x.Market_id + ":" + x.Identifier)))}");
        }

        /// <summary>
        /// Get all public trades (all trades done on the marketplace) beloning to one ore more tradable.
        /// </summary>
        /// <param name="tradable"></param>
        /// <param name="count">Count of 0 equals all trades</param>
        /// <returns></returns>
        public Task<List<PublicTrade>> GetAllPublicTradesForTradable(Tradable tradable, long count = 5)
        {
            return GetAllPublicTradesForTradables(new List<Tradable> { tradable }, count);
        }

        /// <summary>
        /// Get all public trades (all trades done on the marketplace) beloning to one ore more tradable.
        /// </summary>
        /// <param name="tradables"></param>
        /// <param name="count">Count of 0 equals all trades</param>
        /// <returns></returns>
        public async Task<List<PublicTrade>> GetAllPublicTradesForTradables(List<Tradable> tradables, long count = 5)
        {
            string tmpCount = count.ToString();
            if(count == 0)
            {
                tmpCount = "all";
            }

            return await GetData<List<PublicTrade>>($"/next/2/tradables/trades/{HttpUtility.UrlEncode(string.Join(",", tradables.Select(x => x.Market_id + ":" + x.Identifier)))}?count={tmpCount}");
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
            if (disposing)
            {
                keepAliveTimer?.Dispose();
                client?.Dispose();
            }
        }
    }
}

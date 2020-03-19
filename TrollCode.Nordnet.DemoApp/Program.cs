using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Trollcode.Nordnet.API;
using Trollcode.Nordnet.API.Responses;

namespace Trollcode.Nordnet.DemoApp
{
    class Program
    {
        // The public key for NEXTAPI from the XML file
        static readonly string PUBLIC_KEY =
          "<?xml version=\"1.0\"?>" +
          "<RSAParameters xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
          "<Exponent>AQAB</Exponent>" +
          "<Modulus>5td/64fAicX2r8sN6RP3mfHf2bcwvTzmHrLcjJbU85gLROL+IXclrjWsluqyt5xtc/TCwM" +
          "TfC/NcRVIAvfZdt+OPdDoO0rJYIY3hOGBwLQJeLRfruM8dhVD+/Kpu8yKzKOcRdne2hBb/mpkVtIl5av" +
          "JPFZ6AQbICpOC8kEfI1DHrfgT18fBswt85deILBTxVUIXsXdG1ljFAQ/lJd/62J74vayQJq6l2DT663Q" +
          "B8nLEILUKEt/hQAJGU3VT4APSfT+5bkClfRb9+kNT7RXT/pNCctbBTKujr3tmkrdUZiQiJZdl/O7LhI9" +
          "9nCe6uyJ+la9jNPOuK5z6v72cXenmKZw==</Modulus>" +
          "</RSAParameters>";

        /// <summary>
        /// Get a Nordnet Context that can be used to access the Nordnet API <br/>
        /// -- Created as a way of hiding the username and password retrieved from KeyVault from local stack when live streaming --
        /// </summary>
        /// <param name="user">Username. Or DemoSettings:NordnetUsername from appsettings will be used</param>
        /// <param name="pass">Password. Or DemoSettings:NordnetPassword from appsettings will be used</param>
        /// <returns></returns>
        static async Task<NordnetApi> LoginAndCreateNordnetContext(string user = null, string pass = null)
        {
            string username = user;
            string password = pass;

            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password))
            {
                AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();

                try
                {
                    IConfigurationRoot configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .AddUserSecrets<Program>()
                       .Build();

                    string usernameUri = configuration.GetSection("DemoSettings:NordnetUsername").Value;
                    string passwordUri = configuration.GetSection("DemoSettings:NordnetPassword").Value;

                    if (string.IsNullOrWhiteSpace(usernameUri) || string.IsNullOrWhiteSpace(passwordUri))
                    {
                        throw new Exception("Could find any KeyVault URI's in config. Please add DemoSettings:NordnetUsername and DemoSettings:NordnetPassword to appsetting.json");
                    }

                    var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));


                    username = (await keyVaultClient.GetSecretAsync(usernameUri)).Value;
                    password = (await keyVaultClient.GetSecretAsync(passwordUri)).Value;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception while getting credentials from KeyVault: {ex.Message}");
                }
            }
            NordnetApi api = new NordnetApi(
                                NordnetApi.GetCryptoserviceProviderForParameterXML(PUBLIC_KEY),
                                "https://api.test.nordnet.se"
                            );

            await api.LoginAsync(username, password);
            return api;
        }

        static async Task Main(string[] args)
        {
            try
            {
                using (NordnetApi nordnet = await LoginAndCreateNordnetContext())
                {
                    //If no exception was thrown. We are logged in and have a valid session
                    nordnet.OnSessionDisconnected += Nordnet_OnSessionDisconnected;

                    List<InstrumentList> lists = await nordnet.GetInstrumentLists();

                    var accesses = await nordnet.GetMarketRealtimeAccessList();

                    // Mid cap oslo list:  16384830  16314769
                    // Large cap Oslo: 16384829
                    // Small cap Oslo: 16384831

                    //List<Instrument> instruments_no = await nordnet.GetInstrumentsInList(16384831);
                    //List<Instrument> instruments_dk = await nordnet.GetInstrumentsInList(16314769);


                    //await nordnet.GetMarkets();

                    //foreach (var instrument in instruments_dk)
                    //{
                    //    //2014-06-20
                    //    List<OptionPair> optionPair = await nordnet.GetOptionPairsForInstrument(instrument.Instrument_id);
                    //    if ((optionPair?.Count() ?? 0) > 0)
                    //    {
                    //        Console.WriteLine("");
                    //    }
                    //    await Task.Delay(250);
                    //}

                    //var sectors = await nordnet.GetAllInstrumentSectors("FINANCIAL");
                    //var instrumentTypes = await nordnet.GetAllInstrumentTypes();



                    //var news = await nordnet.GetNews();








                    //var status = await nordnet.GetSystemStatus();
                    //var accounts = await nordnet.GetAccounts();
                    //var accountinfo = await nordnet.GetAccountInfo(accounts[0].Accno);
                    //var ledgerinfo = await nordnet.GetLedgerInformationForAccount(accounts[0].Accno);
                    //var orders = await nordnet.GetOrdersForAccount(accounts[0].Accno);
                    //var orderresult = await nordnet.PostOrder(accounts[0].Accno, new SendOrder());






                    Console.WriteLine("Just for debug stop");
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine($"Something went wrong: {exp.Message}");
            }

            Console.ReadKey();
        }

        private static void Nordnet_OnSessionDisconnected(object sender, EventArgs e)
        {
            Console.WriteLine("Our session was disconnected");
        }
    }
}

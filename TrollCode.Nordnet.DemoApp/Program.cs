using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TrollCode.Nordnet.API;
using TrollCode.Nordnet.API.Responses;

namespace TrollCode.Nordnet
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

            if(string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password))
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

                    var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));


                    username = (await keyVaultClient.GetSecretAsync(usernameUri)).Value;
                    password = (await keyVaultClient.GetSecretAsync(passwordUri)).Value;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Exception while getting credentials from KeyVault: {ex.Message}");
                }
            }
            NordnetApi api = new NordnetApi(
                                NordnetApi.GetCryptoserviceProviderForParameterXML(PUBLIC_KEY),
                                "https://api.test.nordnet.se"
                            );

            await api.Login(username, password);
            return api;
        }

        static async Task Main(string[] args)
        {
            try
            {
                using (NordnetApi nordnet = await LoginAndCreateNordnetContext())
                {
                    //If no exception was thrown. We are logged in and have a valid session

                    List<IntrumentListReponse> lists = await nordnet.GetIntrumentLists();
                    //await nordnet.GetMarkets();

                    Parallel.Invoke(() =>
                    {
                        Console.WriteLine("Setting up private feed");
                        try
                        {
                            new NordnetFeed().ConnectToFeed(
                                nordnet.CurrentSession.PublicFeed, 
                                nordnet.CurrentSession.SessionId
                                );
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                    }, () =>
                    {
                        Console.WriteLine("Setting up public feed");
                        //NordnetFeed publicFeed = new NordnetFeed(nordnet.CurrentSession);
                    });
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine($"Something went wrong: {exp.Message}");
            }

            Console.ReadKey();
        }
    }
}

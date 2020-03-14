using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trollcode.Nordnet.API;

namespace Trollcode.Nordnet.DemoUI_Net47
{
    public class CredentialsHelper : IDisposable
    {
        private readonly KeyVaultClient keyVaultClient;
        private readonly Principal user;

        public CredentialsHelper()
        {
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            user = azureServiceTokenProvider.PrincipalUsed;
        }

        public Principal CurrentUser
        {
            get
            {
                return user;
            }
        }

        public async Task<string> GetKeyVaultSecretAsync(string uri)
        {
            return (await keyVaultClient.GetSecretAsync(uri)).Value;
        }

        public void Dispose()
        {
            Dispose(true);
            //Just in case an inherited class adds a finalizer
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                keyVaultClient?.Dispose();
            }
        }
    }
}

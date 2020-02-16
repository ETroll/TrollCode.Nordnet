
Create a new appsettings.json file in the DemoApp folder.

```bash
touch appsettings.json
```

Add the following config:

```json
{
    //Locations in the KeyVault for the username and password
    "DemoSettings": {
        "NordnetUsername": "<URL to KeyVault secret>",
        "NordnetPassword": "<URL to KeyVault secret>"
    }
}
```

## Authenticating to Azure Services

Local machines don't support managed identities for Azure resources. As a result, the `Microsoft.Azure.Services.AppAuthentication` library uses your developer credentials to run in your local development environment. When the solution is deployed to Azure, the library uses a managed identity to switch to an OAuth 2.0 client credential grant flow. This approach means you can test the same code locally and remotely without worry.

For local development, `AzureServiceTokenProvider` fetches tokens using Visual Studio, Azure command-line interface (CLI), or Azure AD Integrated Authentication. Each option is tried sequentially and the library uses the first option that succeeds. If no option works, an `AzureServiceTokenProviderException` exception is thrown with detailed information.
Authenticating with Visual Studio

### To authenticate by using Visual Studio:

    Sign in to Visual Studio and use Tools > Options to open Options.

    Select Azure Service Authentication, choose an account for local development, and select OK.

If you run into problems using Visual Studio, such as errors that involve the token provider file, carefully review the preceding steps.

You may need to reauthenticate your developer token. To do so, select Tools > Options, and then select Azure Service Authentication. Look for a Re-authenticate link under the selected account. Select it to authenticate.

### Authenticating with Azure CLI

To use Azure CLI for local development, be sure you have version Azure CLI v2.0.12 or later.

To use Azure CLI:

1. Search for Azure CLI in the Windows Taskbar to open the Microsoft Azure Command Prompt.

2. Sign in to the Azure portal: az login to sign in to Azure.

3. Verify access by entering az account get-access-token --resource https://vault.azure.net. If you receive an error, check that the right version of Azure CLI is correctly installed.

If Azure CLI isn't installed to the default directory, you may receive an error reporting that `AzureServiceTokenProvider` can't find the path for Azure CLI. Use the AzureCLIPath environment variable to define the Azure CLI installation folder. `AzureServiceTokenProvider` adds the directory specified in the AzureCLIPath environment variable to the Path environment variable when necessary.

4. If you're signed in to Azure CLI using multiple accounts or your account has access to multiple subscriptions, you need to specify the subscription to use. Enter the command az account set --subscription .

This command generates output only on failure. To verify the current account settings, enter the command `az account list`.

## Notes:

``` Bash

az login

```

``` Powershell
Connect-AzAccount

```

https://docs.microsoft.com/en-us/azure/key-vault/service-to-service-authentication#local-development-authentication
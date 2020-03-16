using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trollcode.Nordnet.API;
using Trollcode.Nordnet.API.FeedModels;
using Trollcode.Nordnet.API.Responses;

namespace Trollcode.Nordnet.DemoUI_Net47
{
    public partial class MainForm : Form, IObserver<FeedResponse>
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

        private NordnetApi api = null;
        private PublicFeed publicFeed = null;
        private PrivateFeed privateFeed = null;
        private NordnetSession session = null;
        private readonly CredentialsHelper credentials = new CredentialsHelper();

        //tmp;
        IDisposable unsubscriber;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            tsStatusLabel.Text = "";
            tsUserLabel.Text = "";
            tbEnvironmentCombo.SelectedIndex = 0;
            gbAccountInfo.Visible = false;

            //TODO: Login using button, and select test or production
            api = new NordnetApi(
                NordnetApi.GetCryptoserviceProviderForParameterXML(PUBLIC_KEY),
                "https://api.test.nordnet.se"
            );

            try
            {
                //TODO: If no uri's found, inform user by status strip
                string usernameUri = ConfigurationManager.AppSettings["DemoSettings:NordnetUsername"];
                string passwordUri = ConfigurationManager.AppSettings["DemoSettings:NordnetPassword"];

                //tsUserLabel.Text = credentials.CurrentUser.UserPrincipalName;

                if (!string.IsNullOrWhiteSpace(usernameUri) && !string.IsNullOrWhiteSpace(passwordUri))
                {
                    string username = await credentials.GetKeyVaultSecretAsync(usernameUri);
                    string password = await credentials.GetKeyVaultSecretAsync(passwordUri);

                    tsStatusLabel.Text = "Logging in to Nordnet";

                    session = await api.LoginAsync(username, password);

                    tsStatusLabel.Text = "Logged in at Nordnet OK";


                    var accounts = await api.GetAccounts();
                    cbAccountsList.DisplayMember = "Name";
                    cbAccountsList.Items.AddRange(accounts.Where(x => !x.Is_blocked).Select(x => new
                    {
                        Name = $"{x.Accno}-{x.Alias} ({x.Type})",
                        Value = x
                    }).ToArray());

                    publicFeed = new PublicFeed(session.PrivateFeed.Hostname, (int)session.PrivateFeed.Port, session.SessionId);

                    unsubscriber = publicFeed.Subscribe(this);

                    publicFeed.Connect();
                }
                else
                {
                    MessageBox.Show("Could not read KeyVault URI's from config", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                tsStatusLabel.Text = "Could not log in to Nordnet";
                MessageBox.Show(ex.Message, "Error during startup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void cbAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var account = (((sender as ComboBox)?.SelectedItem as dynamic).Value as Account);
            gbAccountInfo.Text = $"{account.Accno}-{account.Alias} ({account.Type})";
            gbAccountInfo.Visible = true;

            var accountInfo = await api.GetAccountInfo(account.Accno);

            lblTxtCurrency1.Text = accountInfo.Account_currency;
            lblTxtCurrency2.Text = accountInfo.Account_currency;
            lblTxtCurrency3.Text = accountInfo.Account_currency;

            lblAccountLoanLimit.Text = accountInfo.Loan_limit.Value.ToString();
            lblAccountTradeAvail.Text = accountInfo.Trading_power.Value.ToString();
            lblAccountValue.Text = accountInfo.Own_capital.Value.ToString();



        }

        delegate void OnNextCallback(FeedResponse value);
        public void OnNext(FeedResponse value)
        {
            if(lvPublicFeed.InvokeRequired)
            {
                OnNextCallback d = new OnNextCallback(OnNext);
                Invoke(d, new object[] { value });
            }
            else
            {
                List<string> data = new List<string>();
                data.Add(DateTime.Now.ToShortTimeString());
                data.Add(value.Type);
                data.AddRange(value.Data.Select(x => $"{x.Key}: {x.Value} ").ToList());


                lvPublicFeed.Items.Add(new ListViewItem(data.ToArray()));
            }
        }

        public void OnError(Exception error)
        {
            MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}

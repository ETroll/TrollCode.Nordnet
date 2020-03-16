using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Trollcode.Nordnet.API.FeedModels;

namespace Trollcode.Nordnet.API
{
    public class PublicFeed : NordnetFeed
    {
        private readonly string hostname;
        private readonly int port;
        private readonly string sessionid;

        private readonly CancellationTokenSource cts = new CancellationTokenSource();

        public PublicFeed(string hostname, int port, string sessionid)
        {
            this.hostname = hostname;
            this.port = port;
            this.sessionid = sessionid;
        }

        public void Connect()
        {
            ConnectToFeedAndStart(hostname, port, cts.Token);
            SendGenericCommand(new Command
            {
                cmd = "login",
                args = new Dictionary<string, string>
                {
                    {"session_key", sessionid}
                }
            });
        }





    }
}

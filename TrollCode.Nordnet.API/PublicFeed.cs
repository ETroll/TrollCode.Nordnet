using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Trollcode.Nordnet.API.FeedModels;

namespace Trollcode.Nordnet.API
{
    public class PublicFeed : NordnetFeed
    {
        public void StartSubscriptionToPriceFeed()
        {
            SendGenericCommand(new Command
            {
                cmd = "subscribe",
                args = new Dictionary<string, string>
                {
                    { "t", "price" },
                    { "i", "1869" },
                    { "m", "30"}
                }
            });
        }
        public void StopSubscriptionToPriceFeed()
        {

        }

        public void StartSubscriptionToOrderDepthFeed()
        {

        }
        public void StopSubscriptionToOrderDepthFeed()
        {

        }

        public void StartSubscriptionToTradeFeed()
        {

        }
        public void StopSubscriptionToTradeFeed()
        {

        }
        public void StartSubscriptionToIndicatorFeed()
        {

        }
        public void StopSubscriptionToIndicatorFeed()
        {

        }

        public void StartSubscriptionToNewsFeed()
        {

        }
        public void StopSubscriptionToNewsFeed()
        {

        }

        public void StartSubscriptionToTradingStatusFeed()
        {

        }
        public void StopSubscriptionToTradingStatusFeed()
        {

        }
    }
}

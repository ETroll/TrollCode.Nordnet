using System;
using System.Collections.Generic;
using System.Text;
using Trollcode.Nordnet.API.Responses;

namespace Trollcode.Nordnet.API
{
    public class NordnetSession
    {
        public string SessionId { get; set; }
        public string Environment { get; set; }
        public string Country { get; set; }
        public FeedInformation PrivateFeed { get; set; }
        public FeedInformation PublicFeed { get; set; }
    }
}

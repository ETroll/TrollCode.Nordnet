using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
{
    public class LoginResponse
    {
        public string Session_key { get; set; }
        public int Expires_in { get; set; }
        public string Environment { get; set; }
        public string Country { get; set; }
        public FeedInformation Private_feed { get; set; }
        public FeedInformation Public_feed { get; set; }
    }

    public class FeedInformation
    {
        public string Hostname { get; set; }
        public int Port { get; set; }
        public bool Encrypted { get; set; }
    }
}

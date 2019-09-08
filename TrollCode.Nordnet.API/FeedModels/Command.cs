using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.FeedModels
{
    public class Command
    {
        public string cmd { get; set; }
        public Dictionary<string, string> args { get; set; }
    }
}

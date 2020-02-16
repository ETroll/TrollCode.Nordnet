using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.FeedModels
{
    //{"type":"heartbeat","data":{}}
    public class FeedResponse
    {
        //"price", "depth", "trade", "indicator", "news", "trading_status", "heartbeat"
        public string Type { get; set; }
        public Dictionary<string, string> Data { get; set;}
    }
}

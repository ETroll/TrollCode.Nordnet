using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.FeedModels
{
    public enum FeedReponseType
    {
        PRICE,
        DEPTH,
        TRADE,
        INDICATOR,
        NEWS,
        TRADESTATUS,
        HEARTBEAT
    }
    //{"type":"heartbeat","data":{}}
    public class FeedResponseEnvelope
    {
        //"price", "depth", "trade", "indicator", "news", "trading_status", "heartbeat"
        public string Type { get; set; }
        public Dictionary<string, string> Data { get; set;}
    }
}

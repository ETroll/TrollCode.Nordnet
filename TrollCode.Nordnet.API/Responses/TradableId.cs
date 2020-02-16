using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
     TradableId {
        identifier (string): The Nordnet tradable identifier,
        market_id (integer): The Nordnet market_id
        }
     */
    public class TradableId
    {
        public string Identifier { get; set; }
        public long Market_id { get; set; }
    }
}

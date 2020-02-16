using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        PublicTrades {
            market_id (integer): The Nordnet unique identifier of market,
            identifier (string): The tradable identifier. Market_id + identifier is unique,
            trades (array[PublicTrade]): A list of the public trades
        }
     */
    public class PublicTrade
    {
        /// <summary>
        /// The Nordnet unique identifier of market
        /// </summary>
        public long Market_id { get; set; }

        /// <summary>
        /// The tradable identifier. Market_id + identifier is unique
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// A list of the public trades
        /// </summary>
        public List<PublicTradeInformation> Trades { get; set; }
    }
}

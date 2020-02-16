using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        IntradayGraph {
            market_id (integer): Unique market id,
            identifier (string): The tradable identifier. Market_id + identifier is unique,
            ticks (array[IntradayTick]): A list of price ticks
        }
     */
    public class IntradayTrades
    {
        /// <summary>
        /// Unique market id
        /// </summary>
        public long Market_id { get; set; }

        /// <summary>
        /// The tradable identifier. Market_id + identifier is unique
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// A list of price ticks
        /// </summary>
        public List<IntradayTick> Ticks { get; set; }
    }
}

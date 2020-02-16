using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        IntradayTick {
            timestamp (integer): The timestamp in UTC,
            last (number): Last price during this minute,
            low (number): Lowest price during this minute,
            high (number): Highest price during this minute,
            volume (integer): Traded volume during this minute,
            no_of_trades (integer): No of trades during this minute
        }
    */
    public class IntradayTick
    {
        /// <summary>
        /// The timestamp in UTC
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// Last price during this minute
        /// </summary>
        public decimal Last { get; set; }

        /// <summary>
        /// Lowest price during this minute
        /// </summary>
        public decimal Low { get; set; }

        /// <summary>
        /// Highest price during this minute
        /// </summary>
        public decimal High { get; set; }

        /// <summary>
        /// Traded volume during this minute
        /// </summary>
        public long Volume { get; set; }

        /// <summary>
        /// No of trades during this minute
        /// </summary>
        public long No_of_trades { get; set; }
    }
}

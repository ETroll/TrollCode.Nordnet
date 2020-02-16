using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        TicksizeInterval {
            decimals (integer): No of decimals used in this interval,
            from_price (number): The interval is valid from this price,
            to_price (number): The internval is valid to this price,
            tick (number): The ticksize
        }
     */

    public class TicksizeInterval
    {
        /// <summary>
        /// No of decimals used in this interval
        /// </summary>
        public long Decimals { get; set; }

        /// <summary>
        /// The interval is valid from this price
        /// </summary>
        public decimal From_price { get; set; }

        /// <summary>
        /// The internval is valid to this price
        /// </summary>
        public decimal To_price { get; set; }

        /// <summary>
        /// The ticksize
        /// </summary>
        public decimal Tick { get; set; }
    }
}

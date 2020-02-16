using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        OptionPair {
            strike_price (number): The strike price for this option pair,
            expiration_date (string): The expiration date for this option pair,
            call (Instrument): The call option,
            put (Instrument): The put option
        }
     */
    public class OptionPair
    {
        /// <summary>
        /// The strike price for this option pair
        /// </summary>
        public decimal Strike_price { get; set; }
        /// <summary>
        /// The expiration date for this option pair
        /// </summary>
        public string Expiration_date { get; set; }
        /// <summary>
        /// The call option
        /// </summary>
        public Instrument Call { get; set; }
        /// <summary>
        /// The put option
        /// </summary>
        public Instrument Put { get; set; }
    }
}

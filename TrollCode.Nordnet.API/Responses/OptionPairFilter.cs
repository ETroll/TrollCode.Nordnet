using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
{
    /*
        OptionPairFilter {
        expiration_dates (array[string]): List of valid expiry dates
        }
     */
    public class OptionPairFilter
    {
        /// <summary>
        /// List of valid expiry dates
        /// </summary>
        public List<string> Expiration_dates { get; set; }
    }
}

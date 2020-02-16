using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
    Country {
        country (string): The country code,
        name (string): The translated name of the country
    }
    */
    public class CountryInformation
    {
        /// <summary>
        /// The country code
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// The translated name of the country
        /// </summary>
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        OrderType {
            type (string): The order type code,
            name (string): The translated order type
        }
     */
    public class AllowedOrderType
    {
        /// <summary>
        /// The order type code
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The translated order type
        /// </summary>
        public string Name { get; set; }
    }
}

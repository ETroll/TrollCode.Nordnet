using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        PublicTrade {
            broker_buying (string, optional): Buying participate,
            broker_selling (string, optional): Selling participate,
            volume (integer): The volume of the trade,
            price (number): The price of the trade,
            trade_id (string): The trade id on the exchange,
            trade_type (string, optional): The trade type defined by the exchange.
        }
     */
    public class PublicTradeInformation
    {
        /// <summary>
        /// Buying participate
        /// </summary>
        public string Broker_buying { get; set; }

        /// <summary>
        /// Selling participate
        /// </summary>
        public string Broker_selling { get; set; }

        /// <summary>
        /// The volume of the trade
        /// </summary>
        public long Volume { get; set; }

        /// <summary>
        /// The price of the trade
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The trade id on the exchange
        /// </summary>
        public string Trade_id { get; set; }

        /// <summary>
        /// The trade type defined by the exchange
        /// </summary>
        public string Trade_type { get; set; }
    }
}

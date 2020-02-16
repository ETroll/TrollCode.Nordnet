using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        Trade {
            accno (integer): The Nordnet account number,
            order_id (integer): Nordnet order id,
            trade_id (string, optional): Trade id from the market if available,
            tradable (TradableId): The tradable identification,
            price (Amount): The price of the trade,
            volume (number): The original volume of the order,
            side (string): BUY or SELL,
            counterparty (string, optional): The counterparty if available,
            tradetime (integer): The time of the trade, UNIX time in milliseconds
        }
     */
    public class Trade
    {
        /// <summary>
        /// The Nordnet account number
        /// </summary>
        public long Accno { get; set; }
        /// <summary>
        /// Nordnet order id
        /// </summary>
        public long Order_id { get; set; }
        /// <summary>
        /// Trade id from the market if available
        /// </summary>
        public string Trade_id { get; set; }
        /// <summary>
        /// The tradable identification
        /// </summary>
        public TradableId Tradable { get; set; }
        /// <summary>
        /// The price of the trade
        /// </summary>
        public Amount Price { get; set; }
        /// <summary>
        /// The original volume of the order
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// BUY or SELL
        /// </summary>
        public OrderSideType Side { get; set; }
        /// <summary>
        /// The counterparty if available
        /// </summary>
        public string Counterparty { get; set; }
        /// <summary>
        /// The time of the trade, UNIX time in milliseconds
        /// </summary>
        public long Tradetime { get; set; }
    }
}

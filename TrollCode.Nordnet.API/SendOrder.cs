using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API
{
    public class SendOrder
    {
        /// <summary>
        /// Enter a new order, market_id + identifier is the identifier of the tradable.
        /// </summary>
        public string Identifier { get; set; }
        public int Market_id { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int Volume { get; set; }
        public OrderSideType Side { get; set; }
        public OrderType Order_type { get; set; }
        /// <summary>
        /// 	Date on format YYYY-MM-DD, if left out the order is a day order - 
        /// 	same behavior as if valid_date should be set to today. Smart-orders can only be day orders
        /// </summary>
        public DateTime Valid_until { get; set; }
        /// <summary>
        /// The visible part of an iceberg order. If left out the whole volume of the order is 
        /// visible on the market. This field is only allowed if the order type is LIMIT or NORMAL
        /// </summary>
        public int Open_volume { get; set; }
        /// <summary>
        /// Free text reference for the order. Intended for the end user
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Used for stop loss orders.
        ///     STOP_ACTPRICE_PERC - Trailing stop loss.The order is activated when the price changes by percent.The fields target_value, trigger_value and trigger_condition is required and the price field should not be setSTOP_ACTPRICE - The order is activated when the market price of the instrument reaches a trigger price.The fields trigger_value, trigger_condition and price is required
        ///     MANUAL - The order is inactive in the Nordne system until it is manually activated by a user
        ///     OCO_STOP_ACTPRICE - One cancels other orders - Our implementation is one normal order and one stop loss order. If the real order is executed the stop loss is cancelled.If the stop loss triggers the normal order is canceled.This combo is always displayed as 1 order
        /// </summary>
        public string Activation_condition { get; set; }
        /// <summary>
        /// If type is STOP_ACTPRICE_PERC the value is the given in percent. Minimum value is 1 for STOP_ACTPRICE_PERC. 
        /// If type is STOP_ACTPRICE the value is a fixed price
        /// </summary>
        public decimal? Trigger_value { get; set; }
        /// <summary>
        /// The comparision that should be used on trigger_value <= or >=
        /// </summary>
        public string Trigger_condition { get; set; }
        /// <summary>
        /// Only used when type is STOP_ACTPRICE_PERC or OCO_STOP_ACTPRICE. This is the price on the market.
        /// If type is STOP_ACTPRICE_PERC the value is given in percent. 
        /// The price will be trailing_value + (target_value% of trailing_value). 
        /// If type is OCO_STOP_ACTPRICE the price is a fixed price
        /// </summary>
        public string Target_value { get; set; }

    }

    public enum OrderSideType
    {
        BUY,
        SELL
    }

    /*
     NORMAL is the default if order_type is left out, when using NORMAL the system guess the order type 
     based on used parameters. In order to get better parameter validation and to ensure that the order type 
     is the desired the client should not use NORMAL, please user the intended order type. 
     NORMAL will be deprecated in future versions
     */
    public enum OrderType
    {
        FAK,
        FOK,
        NORMAL,
        LIMIT,
        STOP_LIMIT,
        STOP_TRAILING,
        OCO
    }
}

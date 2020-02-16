using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
     Order {
    accno (integer): The Nordnet account number,
    order_id (integer): Nordnet order id,
    price (Amount): The price of the order,
    volume (number): The original volume of the order,
    tradable (TradableId): Identifier of the tradable,
    open_volume (number, optional): Open volume of an iceberg order,
    traded_volume (number): Total traded volume of the order,
    side (string): BUY or SELL,
    modified (integer): Last modification time of the order. UNIX time in milliseconds,
    reference (string, optional): User reference on the orders, free text field for the end user,
    activation_condition (ActivationCondition, optional): Activation condition for stop loss orders,
    price_condition (string): Price condition on the order:
     LIMIT - The order is limited by the given price
     AT_MARKET - The order is entered at the current market price. This is not supported by most markets,
    volume_condition (string): Volume condition on the order
     NORMAL - All types of fills are accepted
     ALL_OR_NOTHING - Partial fills not accepted,
    validity (Validity): The validity period for the order,
    action_state (string): The state of the last action performed on the order.
     DEL_FAIL - Delete request failed and the order is still active on market
     DEL_PEND - Delete request in progress and unconfirmed from the market
     DEL_CONF - Delete confirmed by market
     DEL_PUSH - Deleted by market
     INS_FAIL - Insert failed
     INS_PEND - Pending insert
     INS_CONF - Confirmed insert
     INS_STOP - The order inserted into the Nordnet system and stopped. This is the state of inactive orders and not triggered stop loss orders
     MOD_FAIL - Modification failed and the previous order values are still valid
     MOD_PEND - Modification in progress and waiting confirmation from market
     MOD_PUSH - Modified by market
     INS_WAIT - Insert waiting for market open
     MOD_WAIT - Modification of order on market waiting for market open
     DEL_WAIT - Delete of order on market waiting for market open
     MOD_CONF - Modification confirmed by the market,
    order_type (string): The order type described a standard combinations of parameters, like fill and kill will have a certain validity and volume conditions. These predefined combinations of parameters can also be used for validation of input.,
    order_state (string): The state of the order:
     DELETED - Order is deleted
     LOCAL - Order is offline/local and eligible for activation
     ON_MARKET - Order is active on market
    }
     */
    public class Order
    {
        public long Accno { get; set; }
        public long Order_id { get; set; }
        public Amount Price { get; set; }
        public decimal Volume { get; set; }
        public TradableId Tradable { get; set; }
        public decimal? Open_volume { get; set; }
        public decimal Traded_volume { get; set; }
        public string Side { get; set; }
        public long Modified { get; set; }
        public string Reference { get; set; }
        public ActivationCondition Activation_condition { get; set; }
        public string Price_condition { get; set; }
        public string Volume_condition { get; set; }
        public Validity Validity { get; set; }
        public ActionStateType Action_state { get; set; }
        public string Order_type { get; set; }
        public OrderstateType Order_state { get; set; }
    }

    public enum ActionStateType
    {
        DEL_FAIL,
        DEL_PEND,
        DEL_CONF,
        DEL_PUSH,
        INS_FAIL,
        INS_PEND,
        INS_CONF,
        INS_STOP,
        MOD_FAIL,
        MOD_PEND,
        MOD_PUSH,
        INS_WAIT,
        MOD_WAIT,
        DEL_WAIT,
        MOD_CONF
    }

    public enum OrderstateType
    {
        DELETED,
        LOCAL,
        ON_MARKET
    }
}

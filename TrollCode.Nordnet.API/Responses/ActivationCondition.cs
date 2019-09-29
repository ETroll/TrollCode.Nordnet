using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
{
    /*
     ActivationCondition {
    type (string): Stop loss activation condition.
     NONE - This order has no activiation condition. It is sent directly to the market (if the market is open)
     MANUAL - The order is inactive in the Nordnet system and is activated by the user
     STOP_ACTPRICE_PERC - Trailing stop loss. The order is activated when the price changes by percent
     STOP_ACTPRICE - The order is activated when the market price of the instrument reaches a trigger price,
    trailing_value (number, optional): Only used when type is STOP_ACTPRICE_PERC. This is the fix point that the trigger_value and target_value percent is calculated from,
    trigger_value (number, optional): If type is STOP_ACTPRICE_PERC the value is the given in percent. If type is STOP_ACTPRICE the value is a fixed price,
    trigger_condition (string, optional): The comparision that should be used on trigger_value <= or >|
    }
     */
    public class ActivationCondition
    {
        public ActivationConditionType Type { get; set; }
        public decimal? Trailing_value { get; set; }
        public decimal? Trigger_value { get; set; }
        public string Trigger_condition { get; set; }
    }

    public enum ActivationConditionType
    {
        NONE,
        MANUAL,
        STOP_ACTPRICE_PERC,
        STOP_ACTPRICE
    }
}

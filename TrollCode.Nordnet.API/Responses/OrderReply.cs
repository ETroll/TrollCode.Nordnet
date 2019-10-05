using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
{
    /*
    OrderReply {
    order_id (integer): The nordnet order id,
    result_code (string): OK or error code,
    order_state (string, optional): The order state. Only returned for valid orders.,
    action_state (string): The action state,
    message (string, optional): Translated error message if result_code is not OK
    }
    */
    public class OrderReply
    {
        public long Order_id { get; set; }
        public string Result_code { get; set; }
        public string Order_state { get; set; }
        public string Action_state { get; set; }
        public string Message { get; set; }
    }
}

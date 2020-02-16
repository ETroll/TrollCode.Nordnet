using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
     
    Amount {
    value (number): The amount value,
    currency (string): The amount currency
    }
     */
    public class Amount
    {
        public decimal Value { get; set; }
        public string Currency { get; set; }
    }
}

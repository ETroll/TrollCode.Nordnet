using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
{
    /*
    Validity {
    type (string): DAY, UNTIL_DATE or IMMEDIATE,
    valid_until (integer, optional): Cancel date, only used when type is UNTIL_DATE
    }
     */
    public class Validity
    {
        public ValitidyType Type { get; set; }
        public long? Valid_until { get; set; }
    }

    public enum ValitidyType
    {
        DAY,
        UNTIL_DATE,
        IMMEDIATE
    }
}

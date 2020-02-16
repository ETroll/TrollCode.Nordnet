using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        UnderlyingInfo {
        instrument_id (integer): Unique identifier of the underlying instrument instrument,
        symbol (string): The symbol of the underlying instrument,
        isin_code (string): The isin code of the underlying instrument
        }
     */
    public class UnderlyingInfo
    {
        public long Instrument_id { get; set; }
        public string Symbol { get; set; }
        public string Isin_code { get; set; }
    }
}

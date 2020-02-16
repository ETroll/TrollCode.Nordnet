using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
    Tradable {
    market_id (integer): Market identifier,
    identifier (string): Nordnet tradable identifier. market_id + identifier is unique,
    tick_size_id (integer): Tick size identifier,
    lot_size (number): The lot size of the tradable,
    display_order (integer): Determine the display order of the tradables for an instrument.
    }
     */
    public class Tradable
    {
        public long Market_id { get; set; }
        public string Identifier { get; set; }
        public long Tick_size_id { get; set; }
        public decimal Lot_size { get; set; }
        public long Display_order { get; set; }
    }
}

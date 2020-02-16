using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
    InstrumentType {
        instrument_type (string): The instrument type code,
        name (string): The translated name of the instrument
    }
     */
    public class InstrumentType
    {
        /// <summary>
        /// The instrument type code
        /// </summary>
        public string Instrument_type { get; set; }
        /// <summary>
        /// The translated name of the instrument
        /// </summary>
        public string Name { get; set; }
    }
}

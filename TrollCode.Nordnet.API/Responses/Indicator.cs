using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
{
    /*
        Indicator {
            name (string): Translated name of the indicator,
            src (string): Indicator source,
            identifier (string): Identifier. source + identifier is unique,
            delayed (integer, optional): The indicator prices is delayed in this many seconds. Defaults to 0. Should be ignored if only_eod is true,
            only_eod (boolean, optional): If true the price is only displayed end-of-day. Defaults to false,
            open (string, optional): The opening time on the format "HH:MM:SS" in UTC if available,
            close (string, optional): The closing time on the format "HH:MM:SS" in UTC if available,
            country (string, optional): The country of the indicator if available,
            type (string): COMMODITY, CURRENCY, INTEREST or INDEX,
            region (string, optional): The region of the indicator if available. Country and region can't be set at the same time,
            instrument_id (integer, optional): Connect the indicator to an instrument. NOTE! Most indicators don't have a real instrument
        }
     */
    public class Indicator
    {
        /// <summary>
        /// Translated name of the indicator
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Indicator source
        /// </summary>
        public string Src { get; set; }
        /// <summary>
        /// Identifier. source + identifier is unique
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// The indicator prices is delayed in this many seconds. Defaults to 0. Should be ignored if only_eod is true
        /// </summary>
        public long? Delayed { get; set; }
        /// <summary>
        /// If true the price is only displayed end-of-day. Defaults to false
        /// </summary>
        public bool? Only_eod { get; set; }
        /// <summary>
        /// The opening time on the format "HH:MM:SS" in UTC if available
        /// </summary>
        public string Open { get; set; }
        /// <summary>
        /// The closing time on the format "HH:MM:SS" in UTC if available
        /// </summary>
        public string Close { get; set; }
        /// <summary>
        /// The country of the indicator if available
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// COMMODITY, CURRENCY, INTEREST or INDEX
        /// </summary>
        public IndicatorType Type { get; set; }
        /// <summary>
        /// The region of the indicator if available. Country and region can't be set at the same time
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// Connect the indicator to an instrument. NOTE! Most indicators don't have a real instrument
        /// </summary>
        public long? Instrument_id { get; set; }
    }

    public enum IndicatorType
    {
        COMMODITY,
        CURRENCY,
        INTEREST,
        INDEX
    }
}

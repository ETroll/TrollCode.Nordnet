using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
    Instrument {
    instrument_id (integer): Unique identifier of the instrument. Can in some cases be 0 if the instrument is not tradable,
    tradables (array[Tradable], optional): The tradables that belongs to the instrument. If the instrument is not tradable this field is left out,
    currency (string): The currency of the instrument,
    instrument_group_type (string, optional): The instrument group. Wider description than instrument type. The description is available in the instrument type lookup,
    instrument_type (string): The instrument type.,
    multiplier (number, optional): The instrument multiplier,
    symbol (string): The instrument symbol. E.g 'ERIC B',
    isin_code (string, optional): The instrument isin code,
    market_view (string, optional): Marking market view for leverage instruments. U for up and D for down,
    strike_price (number, optional): Strike price if applicable,
    pawn_percentage (number, optional): The pawn percentage if applicable,
    number_of_securities (number, optional): Number of securities, not available for all instruments,
    prospectus_url (string, optional): URL to prospectus if available,
    expiration_date (string, optional): Expiration date if applicable.,
    name (string): The instrument name,
    sector (string, optional): The sector id of the instrument,
    sector_group (string, optional): The sector group of the instrument.,
    underlyings (array[UnderlyingInfo], optional): A list of underlyings to the instrument
    }
     */
    public class Instrument
    {
        /// <summary>
        /// Unique identifier of the instrument. Can in some cases be 0 if the instrument is not tradable
        /// </summary>
        public long Instrument_id { get; set; }
        /// <summary>
        /// The tradables that belongs to the instrument. If the instrument is not tradable this field is left out
        /// </summary>
        public List<Tradable> Tradables { get; set; }
        /// <summary>
        /// The currency of the instrument
        /// </summary>
        public string Currency { get; set; }
        //The instrument group. Wider description than instrument type. The description is available in the instrument type lookup
        public string Instrument_group_type { get; set; }
        /// <summary>
        /// The instrument type
        /// </summary>
        public string Instrument_type { get; set; }
        /// <summary>
        /// The instrument multiplier
        /// </summary>
        public decimal? Multiplier { get; set; }
        /// <summary>
        /// The instrument symbol. E.g 'ERIC B'
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// The instrument ISIN code
        /// </summary>
        public string Isin_code { get; set; }
        /// <summary>
        /// Marking market view for leverage instruments. U for up and D for down
        /// </summary>
        public string Market_view { get; set; }
        /// <summary>
        /// Strike price if applicable
        /// </summary>
        public decimal? Strike_price { get; set; }
        /// <summary>
        /// The pawn percentage if applicable
        /// </summary>
        public decimal? Pawn_percentage { get; set; }
        /// <summary>
        /// Number of securities, not available for all instruments,
        /// </summary>
        public decimal? Number_of_securities { get; set; }
        /// <summary>
        /// URL to prospectus if available
        /// </summary>
        public string Prospectus_url { get; set; }
        /// <summary>
        /// Expiration date if applicable
        /// </summary>
        public string Expiration_date { get; set; }
        /// <summary>
        /// The instrument name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The sector id of the instrument
        /// </summary>
        public string Sector { get; set; }
        /// <summary>
        /// The sector group of the instrument
        /// </summary>
        public string Sector_group { get; set; }
        /// <summary>
        /// A list of underlyings to the instrument
        /// </summary>
        public List<UnderlyingInfo> Underlyings { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
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
        public int Instrument_id { get; set; }
        public List<Tradable> Tradables { get; set; }
        public string Currency { get; set; }
        public string Instrument_group_type { get; set; }
        public string Instrument_type { get; set; }
        public decimal? Multiplier { get; set; }
        public string Symbol { get; set; }
        public string Isin_code { get; set; }
        public string Market_view { get; set; }
        public decimal? Strike_price { get; set; }
        public decimal? Pawn_percentage { get; set; }
        public decimal? Number_of_securities { get; set; }
        public string Prospectus_url { get; set; }
        public string Expiration_date { get; set; }
        public string Name { get; set; }
        public string Sector { get; set; }
        public string Sector_group { get; set; }
        public List<UnderlyingInfo> Underlyings { get; set; }
    }
}

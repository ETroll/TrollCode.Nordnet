using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
{
    /*
        LeverageFilter {
            issuers (array[Issuer]): List of valid issuer_id and issuer_name,
            market_view (array[string]): List of valid market views,
            expiration_dates (array[string]): List of valid expiry dates,
            instrument_types (array[string]): List of valid instrument types,
            instrument_group_types (array[string]): List of valid instrument group types,
            currencies (array[string]): List of valid currencies,
            no_of_instruments (integer): Number of derivatives if this filter is used
        }
     */
    public class LeverageFilter
    {
        /// <summary>
        /// List of valid issuer_id and issuer_name
        /// </summary>
        public List<Issuer> Issuers { get; set; }
        /// <summary>
        /// List of valid market views
        /// </summary>
        public List<string> Market_view { get; set; }
        /// <summary>
        /// List of valid expiry dates
        /// </summary>
        public List<string> Expiration_dates { get; set; }
        /// <summary>
        /// List of valid instrument types
        /// </summary>
        public List<string> Instrument_types { get; set; }
        /// <summary>
        /// List of valid instrument group types
        /// </summary>
        public List<string> Instrument_group_types { get; set; }
        /// <summary>
        /// List of valid currencies
        /// </summary>
        public List<string> Currencies { get; set; }
        /// <summary>
        /// Number of derivatives if this filter is used
        /// </summary>
        public long No_of_instruments { get; set; }
    }
}

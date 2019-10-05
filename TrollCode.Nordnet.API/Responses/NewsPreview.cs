using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
{
    /*
        NewsPreview {
            news_id (integer): Unique news id,
            source_id (integer): The Nordnet unique id of the news source,
            headline (string): The news headline,
            instruments (array[integer], optional): List of instrument_ids affected by the news item. Note: this information does not come from all news sources,
            lang (string, optional): The news language. Note: this information does not come from all news sources.,
            type (string): The news type,
            timestamp (integer): The timestamp for the news item. UNIX timestamp in milliseconds
        }
     */
    public class NewsPreview
    {
        /// <summary>
        /// Unique news id
        /// </summary>
        public long News_id { get; set; }
        /// <summary>
        /// The Nordnet unique id of the news source
        /// </summary>
        public long Source_id { get; set; }
        /// <summary>
        /// The news headline
        /// </summary>
        public string Headline { get; set; }
        /// <summary>
        /// List of instrument_ids affected by the news item. Note: this information does not come from all news sources
        /// </summary>
        public List<long> Instruments { get; set; }
        /// <summary>
        /// The news language. Note: this information does not come from all news sources
        /// </summary>
        public string Lang { get; set; }
        /// <summary>
        /// The news type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// The timestamp for the news item. UNIX timestamp in milliseconds
        /// </summary>
        public long Timestamp { get; set; }
    }
}

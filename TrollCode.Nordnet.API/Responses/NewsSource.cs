using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
{
    /*
        NewsSource {
            name (string): The name of the news source,
            source_id (integer): The Nordnet unique id of the news source,
            level (string): The accesslevel for the news source:
                DELAYED - The user can see the news with a 15 minute delay
                REALTIME - The user can see the news in realtime
                FLASH - The user can see FLASH news (FLASH also implies realtime for ordinary news),
            countries (array[string], optional): List of country codes that the news source affect
        }
    */
    public class NewsSource
    {
        /// <summary>
        /// The name of the news source
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Nordnet unique id of the news source
        /// </summary>
        public long Source_id { get; set; }
        /// <summary>
        /// The accesslevel for the news source:
        ///     DELAYED - The user can see the news with a 15 minute delay
        ///     REALTIME - The user can see the news in realtime
        ///     FLASH - The user can see FLASH news(FLASH also implies realtime for ordinary news)
        /// </summary>
        public string Level { get; set; } //TODO: Make enum
    }
}

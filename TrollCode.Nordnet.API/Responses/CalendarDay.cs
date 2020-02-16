using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        CalendarDay {
            date (string): The date on the format YYYY-MM-DD,
            open (integer): The open time in UNIX timestamp (UTC) in milliseconds,
            close (integer): The close time in UNIX timestamp (UTC) in milliseconds
        }
     */
    public class CalendarDay
    {
        /// <summary>
        /// The date on the format YYYY-MM-DD
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// The open time in UNIX timestamp (UTC) in milliseconds
        /// </summary>
        public long Open { get; set; }

        /// <summary>
        /// The close time in UNIX timestamp (UTC) in milliseconds
        /// </summary>
        public long Close { get; set; }
    }
}

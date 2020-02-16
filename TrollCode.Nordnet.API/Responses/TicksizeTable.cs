using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        TicksizeTable {
            tick_size_id (integer): Unique id for the ticksize table,
            ticks (array[TicksizeInterval]): The ticksize interval table
        }
     */

    public class TicksizeTable
    {
        /// <summary>
        /// Unique id for the ticksize table
        /// </summary>
        public long Tick_size_id { get; set; }

        /// <summary>
        /// The ticksize interval table
        /// </summary>
        public List<TicksizeInterval> Ticks { get; set; }
    }
}

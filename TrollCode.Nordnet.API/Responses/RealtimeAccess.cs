using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        RealtimeAccess {
            market_id (integer): Nordnet market_id,
            level (integer): Level 0 - No access, 1 - price access, 2 - order depth
        }
     */
    public class RealtimeAccess
    {
        /// <summary>
        /// Nordnet market id
        /// </summary>
        public long Market_id { get; set; }
        /// <summary>
        /// Access level: Level 0 - No access, 1 - price access, 2 - order depth
        /// </summary>
        public RealtimeAccessLevel Level { get; set; }
    }

    public enum RealtimeAccessLevel
    {
        NO_ACCESS = 0,
        PRICE_ACCESS = 1,
        ORDER_DEPTH = 2
    }
}

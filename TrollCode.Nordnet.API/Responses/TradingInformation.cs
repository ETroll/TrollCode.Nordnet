using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        TradableInfo {
            market_id (integer): The Nordnet unique identifier of market,
            identifier (string): Identifier of the tradable. identifier + market_id is unique,,
            iceberg (boolean): True if iceberg orders is allowed,
            calendar (array[CalendarDay]): Allowed days for long term orders,
            order_types (array[OrderType]): Allowed order types
        }
     */
    public class TradingInformation
    {
        public long Market_id { get; set; }
        public string Identifier { get; set; }
        public bool Iceberg { get; set; }
        public List<CalendarDay> Calendar { get; set; }
        public List<AllowedOrderType> Order_types { get; set; }
    }
}

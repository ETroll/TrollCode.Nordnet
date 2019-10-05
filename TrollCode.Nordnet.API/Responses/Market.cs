using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
{
    public class Market
    {
        public long Market_id { get; set; }
        public string Country { get; set; }
        public bool Is_virtual { get; set; }
        public string Name { get; set; }
    }
}

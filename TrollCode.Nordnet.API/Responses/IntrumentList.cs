using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    public class IntrumentList
    {
        //List {
        //    symbol(string): The short name of the symbol.,
        //    display_order(integer): Used for sorting the list before displaying them to the end user.,
        //    list_id(integer): Unique id for the list,
        //    name (string): The translated name of the list.,
        //    country(string, optional): The country of the list if available.,
        //    region(string, optional): The region of the list if available.
        // }

        public string Symbol { get; set; }
        public long Display_order { get; set; }
        public long List_id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
    }
}

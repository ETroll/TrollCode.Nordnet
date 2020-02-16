using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        Sector {
            sector (string): The unique sector id,
            group (string, optional): The sector group for this sector,
            name (string): Translated name of the sector
        }
     */
    public class SectorType
    {
        /// <summary>
        /// The unique sector id
        /// </summary>
        public string Sector { get; set; }
        /// <summary>
        /// The sector group for this sector
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// Translated name of the sector
        /// </summary>
        public string Name { get; set; }
    }
}

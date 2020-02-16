using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{
    /*
        Issuer {
            name (string): Name of the issuer,
            issuer_id (integer): Unique id of the issuer
        }
     */
    public class Issuer
    {
        /// <summary>
        /// Name of the issuer
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Unique id of the issuer
        /// </summary>
        public long Issuer_id { get; set; }
    }
}

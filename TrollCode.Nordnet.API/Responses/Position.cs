using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
{
    /*
        Position {
            accno (integer): The account number,
            instrument (Instrument): Position instrument,
            qty (number): The quantity of the position,
            pawn_percent (integer): How much can the user loan on this position,
            market_value_acc (Amount): Market value in the account currency,
            market_value (Amount): Market value in the tradable currency,
            acq_price (Amount): Acquisition price in the tradable currency,
            acq_price_acc (Amount): Acquisition price in the account currency,
            morning_price (Amount): The price of the instrument of the position in the morning
        }
     */
    public class Position
    {
        /// <summary>
        /// The account number
        /// </summary>
        public long Accno { get; set; }
        /// <summary>
        /// Position instrument
        /// </summary>
        public Instrument Instrument { get; set; }
        /// <summary>
        /// The quantity of the position
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// How much can the user loan on this position
        /// </summary>
        public Amount Pawn_percent { get; set; }
        /// <summary>
        /// Market value in the account currency
        /// </summary>
        public Amount Market_value_acc { get; set; }
        /// <summary>
        /// Market value in the tradable currency
        /// </summary>
        public Amount Market_value { get; set; }
        /// <summary>
        /// Acquisition price in the tradable currency
        /// </summary>
        public Amount Acq_price { get; set; }
        /// <summary>
        /// Acquisition price in the account currency
        /// </summary>
        public Amount Acq_price_acc { get; set; }
        /// <summary>
        /// The price of the instrument of the position in the morning
        /// </summary>
        public Amount Morning_price { get; set; }
    }
}

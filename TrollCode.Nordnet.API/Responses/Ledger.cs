using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
{
    /*
     Ledger {
    currency (string): Currency of the ledger,
    account_sum (Amount): The sum in the ledger currency,
    account_sum_acc (Amount): The sum in the account currency,
    acc_int_deb (Amount): Interest debit in the ledger currency,
    acc_int_cred (Amount): Interest credit in the ledger currency,
    exchange_rate (Amount): The price to convert to base currency
    }
     */
    public class Ledger
    {
        public string Currency { get; set; }
        public Amount Account_sum { get; set; }
        public Amount Account_sum_acc { get; set; }
        public Amount Acc_int_deb { get; set; }
        public Amount Acc_int_cred { get; set; }
        public Amount Exchange_rate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
{
    /*
     LedgerInformation {
    total_acc_int_deb (Amount): Total interest debit in the account currency,
    total_acc_int_cred (Amount): Total interest credit in the account currency,
    total (Amount): Total of all the ledgers in the account currency,
    ledgers (array[Ledger]): Each ledger
    }
     */
    public class LedgerInformation
    {
        public Amount Total_acc_int_deb { get; set; }
        public Amount Total_acc_int_cred { get; set; }
        public Amount Total { get; set; }
        public List<Ledger> Ledgers { get; set; }
    }
}

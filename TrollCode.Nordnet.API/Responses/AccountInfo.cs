using System;
using System.Collections.Generic;
using System.Text;

namespace TrollCode.Nordnet.API.Responses
{
    /*
     AccountInfo {
    account_currency (string): The account currency,
    account_credit (Amount): Account credit,
    account_sum (Amount): All ledgers combined,
    collateral (Amount): Collateral claim for options,
    credit_account_sum (Amount, optional): Sum for credit account if available,
    forward_sum (Amount): Locked amount for forwards,
    future_sum (Amount): Not realized profit/loss for future,
    unrealized_future_profit_loss (Amount): Unrealized profit and loss for futures,
    full_marketvalue (Amount): Total market value,
    interest (Amount): Interest on the account,
    intraday_credit (Amount, optional): Intraday credit if available,
    loan_limit (Amount): Max loan limit (regardless of pawnvalue),
    own_capital (Amount): account_sum + full_marketvalue + interest + forward_sum + future_sum + unrealized_future_profit_loss,
    own_capital_morning (Amount): Own capital calculated in the morning. Static during the day,
    pawn_value (Amount): Pawn value of all positions combined,
    trading_power (Amount): Available for trading
    }
     */
    public class AccountInfo
    {
        public string Account_currency { get; set; }
        public Amount Account_credit { get; set; }
        public Amount Account_sum { get; set; }
        public Amount Collateral { get; set; }
        public Amount Credit_account_sum { get; set; }
        public Amount Forward_sum { get; set; }
        public Amount Future_sum { get; set; }
        public Amount Unrealized_future_profit_loss { get; set; }
        public Amount Full_marketvalue { get; set; }
        public Amount Interest { get; set; }
        public Amount Intraday_credit { get; set; }
        public Amount Loan_limit { get; set; }
        public Amount Own_capital { get; set; }
        public Amount Own_capital_morning { get; set; }
        public Amount Pawn_value { get; set; }
        public Amount Trading_power { get; set; }
    }
}

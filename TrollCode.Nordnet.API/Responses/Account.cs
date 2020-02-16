using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{

    /*
     Account {
    accno (integer): The account number of the account to use,
    type (string): Translated account type. Can be displayed for the end user,
    default (boolean): True if this is the default account,
    alias (string): Account alias can be set on Nordnet Web by the end user,
    is_blocked (boolean, optional): True if the account is blocked. No queries can be made to this account,
    blocked_reason (string, optional): Description to why the account is blocked. The language specified in the request is used in this reply so it can be displayed to the end user
    } 
     */
    public class Account
    {
        public long Accno { get; set; }
        public string Type { get; set; }
        public bool Default { get; set; }
        public string Alias { get; set; }
        public bool Is_blocked { get; set; }
        public string Blocked_reason { get; set; }
    }
}

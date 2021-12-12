using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Models
{
    public class AccountInfo
    {
        public string _email { get; }
        public string _passphrase { get; }

        public string _accountStatus { get; }

        public AccountInfo(string email, string passPhrase, string accountStatus)
        {
            _email = email;
            _passphrase = passPhrase;
            _accountStatus = accountStatus;
        }

    }
}

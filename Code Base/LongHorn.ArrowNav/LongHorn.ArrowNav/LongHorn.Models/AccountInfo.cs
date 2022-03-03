using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Models
{
    public class AccountInfo
    {
        public string _email { get; }
        public string _passphrase { get; }

        public string _accountType { get; }

        public AccountInfo(string email, string passphrase, string accountType)
        {
            _email = email;
            _passphrase = passphrase;
            _accountType = accountType;
        }
        public AccountInfo(string email, string passphrase)
        {
            _email = email;
            _passphrase = passphrase;
        }
        public AccountInfo(string email)
        {
            _email = email;
        }

    }
}

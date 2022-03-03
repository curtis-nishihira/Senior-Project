using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Managers
{
    public class AuthenticationManager
    {
        public AuthenticationManager()
        {
        }
        public string AuthenAccount(AccountInfo account)
        {
            AuthnService authnService = new AuthnService();
            var result = authnService.ApplyAuthn(account);
            return result;
        }
    }
}
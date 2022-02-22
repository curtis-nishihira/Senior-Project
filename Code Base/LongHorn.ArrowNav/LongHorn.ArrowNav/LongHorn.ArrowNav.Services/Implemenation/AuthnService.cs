using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Services
{
    public class AuthnService : IAuthnService<AccountInfo>
    {
        public string ApplyAuthn(AccountInfo account)
        {
            UMRepository umRepository = new UMRepository();
            var result = umRepository.AuthnAccount(account);
            return result;
        }
        public string confirmEmail(string email)
        {
            UMRepository umRepository = new UMRepository();
            var result = umRepository.confirmUserEmail(email);
            return result;
        }
    }
}
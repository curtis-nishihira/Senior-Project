using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Services
{
    public class AuthzService : IAuthzService<AccountInfo>
    {
        public string ApplyAuthz(AccountInfo account)
        {
            UMRepository umRepository = new UMRepository();
            var result = umRepository.AuthzAccount(account);
            return result;
        }
    }
}
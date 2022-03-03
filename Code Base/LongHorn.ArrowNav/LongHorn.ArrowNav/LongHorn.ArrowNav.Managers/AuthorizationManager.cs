using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Managers
{
    public class AuthorizationManager
    {
        public AuthorizationManager()
        {
        }
        public string AuthzAccount(AccountInfo account)
        {
            AuthzService authzService = new AuthzService();
            var result = authzService.ApplyAuthz(account);
            return result;
        }
    }
}
using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Services
{
    public class AuthnService : IAuthnService<LoginModel>
    {
        public string ApplyAuthn(LoginModel model)
        {
            UMRepository umRepository = new UMRepository();
            var result = umRepository.AuthnAccount(model);
            return result;
        }
        public string confirmEmail(string email)
        {
            UMRepository umRepository = new UMRepository();
            var result = umRepository.confirmUserEmail(email);
            return result;
        }

        public bool isAdmin(LoginModel model)
        {
            UMRepository umRepository = new UMRepository();
            var result = umRepository.AuthorizationLevel(model);
            return result;
        }
    }
}
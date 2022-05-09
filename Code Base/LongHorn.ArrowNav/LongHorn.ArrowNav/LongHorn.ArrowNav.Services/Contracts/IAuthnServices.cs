using System;
using System.Collections.Generic;
using System.Text;
using LongHorn.ArrowNav.Models;

namespace LongHorn.ArrowNav.Services
{
    public interface IAuthnService<T>
    {
        public LoginResponse ApplyAuthn(T model);
        public string confirmEmail(string email);
    }
}
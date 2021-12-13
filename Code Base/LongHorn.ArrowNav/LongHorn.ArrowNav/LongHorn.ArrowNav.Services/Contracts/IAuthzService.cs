using System;
using System.Collections.Generic;
using System.Text;
using LongHorn.ArrowNav.Models;

namespace LongHorn.ArrowNav.Services
{
    public interface IAuthzService<T>
    {
        public string ApplyAuthz(T model);
    }
}
using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Services
{
    public class EnableService : IEnableService
    {
        public string EnableAccount(AccountInfo info)
        {
            UMRepository umRepository = new UMRepository();
            var result = umRepository.Enable(info);
            return result;
        }
    }
}
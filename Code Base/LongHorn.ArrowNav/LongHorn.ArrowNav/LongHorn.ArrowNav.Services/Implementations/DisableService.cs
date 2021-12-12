using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Services
{
    public class DisableService : IDisableService
    {
        public string DisableAccount(AccountInfo info)
        {
            //Could possibly add theses methods to the IRepository.
            UMRepository umRepository = new UMRepository();
            var result = umRepository.Disable(info);
            return result;
        }
    }
}
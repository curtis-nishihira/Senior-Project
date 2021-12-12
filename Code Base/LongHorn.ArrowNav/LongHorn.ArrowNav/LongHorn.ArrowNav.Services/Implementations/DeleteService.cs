using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Services
{
    public class DeleteService : IDeleteService
    {
        public string DeleteAccount(AccountInfo info)
        {
            IRepository<AccountInfo> umRepository = new UMRepository();
            var result = umRepository.Delete(info);
            return result;
        }
    }
}
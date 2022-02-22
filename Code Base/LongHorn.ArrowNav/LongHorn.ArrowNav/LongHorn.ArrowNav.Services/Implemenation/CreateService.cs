using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.DAL;
using System;

namespace LongHorn.ArrowNav.Services
{
    public class CreateService : ICreateService
    {
        public string CreateAccount(AccountInfo info)
        {
            IRepository<AccountInfo> loggingRepository = new UMRepository();
            var result = loggingRepository.Create(info);
            return result;
        }
    }
}
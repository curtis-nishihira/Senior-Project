using LongHorn.ArrowNav.Services;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Managers
{
    public class UserDeleteManager
    {
        public UserDeleteManager()
        {
        }

        public string SaveChanges(AccountInfo account)
        {
            DeleteService createService = new DeleteService();
            var result = createService.DeleteAccount(account);
            Log entry = new Log(result, "Bussiness", "Info", account._email, DateTime.UtcNow);
            //LogManager logManager = new LogManager();
            //execute
            //var actualOutput = logManager.Log(entry);
            return result;
        }
    }
}
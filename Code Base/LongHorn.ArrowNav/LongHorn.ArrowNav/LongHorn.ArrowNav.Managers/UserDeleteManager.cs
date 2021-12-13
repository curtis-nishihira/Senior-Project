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
            LogManager logManager = new LogManager();
            Log entry = new Log(result, "Info", "Data Layer", account._email);
            logManager.Log(entry);
            return result;
        }
    }
}
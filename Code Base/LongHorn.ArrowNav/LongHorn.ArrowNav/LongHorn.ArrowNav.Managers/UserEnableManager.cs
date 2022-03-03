using LongHorn.ArrowNav.Services;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Managers
{
    public class UserEnableManager
    {
        public UserEnableManager()
        {
        }

        public string SaveChanges(AccountInfo account)
        {
            EnableService createService = new EnableService();
            var result = createService.EnableAccount(account);
            LogManager logManager = new LogManager();
            Log entry = new Log(result, "Info", "Data Layer", account._email);
            logManager.Log(entry);
            return result;
        }
    }
}
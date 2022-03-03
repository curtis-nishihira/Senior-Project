using LongHorn.ArrowNav.Services;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Managers
{
    public class UserDisableManager
    {
        public UserDisableManager()
        {
        }

        public string SaveChanges(AccountInfo account)
        {
            DisableService createService = new DisableService();
            var result = createService.DisableAccount(account);
            LogManager logManager = new LogManager();
            Log entry = new Log(result, "Info", "Data Layer", account._email);
            logManager.Log(entry);
            return result;
        }
    }
}
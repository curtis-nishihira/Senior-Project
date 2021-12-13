using LongHorn.ArrowNav.Services;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Managers
{
    public class UserCreateManager
    {
        public UserCreateManager()
        {

        }

        public string SaveChanges(AccountInfo account)
        {
            CreateService createService = new CreateService();
            var result = createService.CreateAccount(account);
            LogManager logManager = new LogManager();
            Log entry = new Log(result, "Info", "Data Layer",account._email);
            logManager.Log(entry);
            return result;
        }
    }
}
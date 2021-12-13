using LongHorn.ArrowNav.Services;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Managers
{
    public class UserUpdateManager
    {
        public UserUpdateManager()
        {
        }

        public string SaveChanges(AccountInfo account)
        {
            UpdateService createService = new UpdateService();
            var result = createService.UpdateAccount(account);
            return result;
        }
    }
}
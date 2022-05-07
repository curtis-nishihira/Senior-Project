using LongHorn.ArrowNav.Managers;
using LongHorn.ArrowNav.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using LongHorn.ArrowNav.Services;
using System.Security.Cryptography;
using System.Text;
using LongHorn.ArrowNav.Models;

namespace LongHorn.ArrowNav.Tests
{
    [TestClass]
    public class UserManagementShould
    {
        [TestMethod]
        public void CreateUserValidAsync()
        {
            LoginModel model = new LoginModel();
            model.Username = "spencergravel@gmail.com";
            model.Password = "123456";
            AuthnService authnService = new AuthnService();
            var result = authnService.ApplyAuthn(model);
            Trace.WriteLine(result);

        }

        //[TestMethod]
        //public void DeleteUserValid()
        //{
        //    UserDeleteManager manager = new UserDeleteManager();
        //    AccountInfo account = new AccountInfo(@"admin2@gmail.com", "pasword", "admin");
        //    var actual = manager.SaveChanges(account);
        //    string expected = "Account was deleted successfully";
        //    Trace.WriteLine(actual);
        //    Assert.IsTrue(actual == expected);
        //}
        //[TestMethod]
        //public void EnableUserValid()
        //{
        //    UserEnableManager manager = new UserEnableManager();
        //    AccountInfo account = new AccountInfo(@"admin@gmail.com", "pasword", "admin");
        //    var actual = manager.SaveChanges(account);
        //    string expected = "Account was enabled";
        //    Trace.WriteLine(actual);
        //    Assert.IsTrue(actual == expected);
        //}
        //[TestMethod]
        //public void DisableUserValid()
        //{
        //    UserDisableManager manager = new UserDisableManager();
        //    AccountInfo account = new AccountInfo(@"admin@gmail.com", "password123", "user");
        //    var actual = manager.SaveChanges(account);
        //    string expected = "Account was disabled";
        //    Trace.WriteLine(actual);
        //    Assert.IsTrue(actual == expected);
        //}

        //[TestMethod]
        //public void DisableUserValid()
        //{
        //    UMManager manager = new UMManager();
        //    var actual = manager.getProfile("spencergravel@gmail.com");
        //    string expected = "Account was disabled";
        //    Trace.WriteLine(actual);
        //}
        [TestMethod]
        public void testRepo()
        {
            ScheduleRepository repo = new ScheduleRepository();
            var result = repo.Read("spencergravel@gmail.com");

        }

    }
}
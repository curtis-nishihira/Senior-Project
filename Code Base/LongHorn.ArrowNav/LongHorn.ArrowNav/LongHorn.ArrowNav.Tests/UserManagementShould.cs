using LongHorn.ArrowNav.Managers;
using LongHorn.ArrowNav.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace LongHorn.ArrowNav.Tests
{
    [TestClass]
    public class UserManagementShould
    {
        //public void CreateUserValid()
        //{
        //    UserCreateManager manager = new UserCreateManager();
        //    AccountInfo account = new AccountInfo(@"admin2@gmail.com", "pasword", "admin");
        //    var actual = manager.SaveChanges(account);
        //    string expected = "Successful Account Creation";
        //    Trace.WriteLine(actual);
        //    Assert.IsTrue(actual == expected);
        //}

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
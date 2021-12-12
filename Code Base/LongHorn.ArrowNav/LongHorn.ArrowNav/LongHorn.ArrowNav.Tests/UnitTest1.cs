using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace LongHorn.ArrowNav.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            UserCreateManager manager = new UserCreateManager();
            AccountInfo account = new AccountInfo(@"admin2@gmail.com", "pasword", "admin");
            var actual = manager.SaveChanges(account);
            string expected = "Successful Account Creation";
            Trace.WriteLine(actual);
            Assert.IsTrue(actual == expected);
        }
        [TestMethod]
        public void TestMethod2()
        {
            UserDeleteManager manager = new UserDeleteManager();
            AccountInfo account = new AccountInfo(@"admin2@gmail.com", "pasword", "admin");
            var actual = manager.SaveChanges(account);
            string expected = "Account was deleted successfully";
            Trace.WriteLine(actual);
            Assert.IsTrue(actual == expected);
        }
        [TestMethod]
        public void TestMethod3()
        {
            UserEnableManager manager = new UserEnableManager();
            AccountInfo account = new AccountInfo(@"admin@gmail.com", "pasword", "admin");
            var actual = manager.SaveChanges(account);
            string expected = "Account was enabled";
            Trace.WriteLine(actual);
            Assert.IsTrue(actual == expected);
        }
        [TestMethod]
        public void TestMethod4()
        {
            UserDisableManager manager = new UserDisableManager();
            AccountInfo account = new AccountInfo(@"admin@gmail.com", "password123", "user");
            var actual = manager.SaveChanges(account);
            string expected = "Account was disabled";
            Trace.WriteLine(actual);
            Assert.IsTrue(actual == expected);
        }

    }
}
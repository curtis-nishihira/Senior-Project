using LongHorn.ArrowNav.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LongHorn.ArrowNav.Tests
{
    [TestClass]
    public class DatabaseLogServiceShould
    {
        [TestMethod]
        public void LogValid()
        {
            ILogService service = new DatabaseLogService();

            var actual = service.Log("Log Entry");

            var expected = true;

            Assert.IsTrue(actual == expected);
        }


    }
}

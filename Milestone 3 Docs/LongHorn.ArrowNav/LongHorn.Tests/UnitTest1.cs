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
            //initialize
            ILogService service = new DatabaseLogService();
            
            //execute
            var actualOutput = service.Log("Log was successful");
            var expectedOutput = true;
            var actualTimeElapsed = service.getElapsedTime();
            var expectedTime = 5;

            //comparision
            Assert.IsTrue(actualOutput == expectedOutput);
            Assert.IsTrue(actualTimeElapsed <= expectedTime);
        }


    }
}

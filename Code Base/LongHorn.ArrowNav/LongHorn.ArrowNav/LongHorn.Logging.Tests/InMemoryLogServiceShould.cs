using LongHorn.ArrowNav.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LongHorn.Logging.Tests
{
    [TestClass]
    public class InMemoryLogServiceShould
    {
        [TestMethod]
        public void LogValidDescription()
        {
            //Arrange
            ILogService logService = new InMemoryLogService();
            var expected = "Successful Log";
            Log logEntry = new Log("Test Log");
            //Act
            var actual = logService.Log(logEntry);

            //Assert
            Assert.IsTrue(expected == actual);
        }
        [TestMethod]
        public void GetAllLogs()
        {
            //Arrange
            ILogService logService = new InMemoryLogService();
            var expectedCount = 1;
            Log logEntry = new Log("Test Log");
            var expectedLogMessage = "Test Log";
            //Act
            var actualLog = logService.Log(logEntry);
            var actualFetch = logService.GetAllLogs();

            //Assert
            Assert.IsTrue(actualFetch.Count == expectedCount);
            Assert.IsTrue(actualFetch[0]._Log.Equals(expectedLogMessage));
        }
        [TestMethod]
        public void GetNoLogs()
        {
            //Arrange
            var stopwatch = new Stopwatch();
            ILogService logService = new InMemoryLogService();
            var expectedCount = 0;
            var expectedSeconds = 5;

            //Act
            stopwatch.Start();
            var actualFetch = logService.GetAllLogs();
            stopwatch.Stop();
            //Assert
            Assert.IsTrue(actualFetch.Count == expectedCount);
            Assert.IsTrue(stopwatch.Elapsed.TotalSeconds <= expectedSeconds);
        }
    }
}

using LongHorn.ArrowNav.Managers;
using LongHorn.ArrowNav.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace LongHorn.ArrowNav.Tests
{
    [TestClass]
    public class DatabaseLogServiceShould
    {
        [TestMethod]
        public void LogValid()
        {
            //initialize
            Log entry = new Log("Test scenario 1 with Objects", "Bussiness", "Info", "Curtis");
            LogManager logManager = new LogManager();
            //execute
            var actualOutput = logManager.Log(entry);
            var expectedOutput = "Successful Log";

            //comparision
            Trace.WriteLine(actualOutput);
            Assert.IsTrue(actualOutput == expectedOutput);
        }
        //Failure test case(Looking for test to fail)
        [TestMethod]
        public void LogNotSaved()
        {
            //intialize
            Log entry = new Log("Test scenario 1 with Objects", "Bussiness", "Info", "Brayan");
            LogManager logManager = new LogManager();
            //execute
            var actualOutput = logManager.Log(entry);
            var expectedOutput = "Log was not saved onto the data store";

            //comparision
            Trace.WriteLine(actualOutput);
            Assert.IsTrue(actualOutput == expectedOutput);

        }

        ////Failure test case(Looking for test to fail)
        [TestMethod]
        public void LogLongerThan5Seconds()
        {
            //intialize
            Log entry = new Log("Test scenario 3 with Objects", "Bussiness", "Info", "Spencer");
            LogManager logManager = new LogManager();
            //execute
            var actualOutput = logManager.Log(entry);
            var expectedOutput = "Logging process took longer than 5 seconds";

            //comparision
            Trace.WriteLine(actualOutput);
            Assert.IsTrue(actualOutput == expectedOutput);
        }






    }
}

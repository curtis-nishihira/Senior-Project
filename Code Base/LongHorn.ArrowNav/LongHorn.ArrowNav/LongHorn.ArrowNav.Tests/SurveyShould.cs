using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace LongHorn.ArrowNav.Tests
{
    [TestClass]
    public class SurveyShould
    {
        [TestMethod]
        public void FillSurveyShould()
        {
            var DriverPath = @"C:\Program Files\Mozilla Firefox";
            IWebDriver driver = new FirefoxDriver(DriverPath);
            driver.Navigate().GoToUrl("https://ArrowNav.AzureWebsites.net");
            driver.FindElement(By.LinkText("Wellness Hub")).Click();
            driver.FindElement(By.LinkText("Submit")).Click();
            var ActualOutput = driver.FindElement(By.LinkText("Survey Completed")).Text;
            var ExpectedOutput = "Survey Completed";
            driver.Close();

            Trace.WriteLine(ActualOutput);
            Assert.IsTrue(ActualOutput == ExpectedOutput);

        }
    }
}

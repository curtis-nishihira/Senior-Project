using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System.Diagnostics;
using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace LongHorn.ArrowNav.Tests
{
    [TestClass]
    public class SurveyShould
    {
        [TestMethod]
        public void FillSurveyShould()
        {
            var driverpath = @"C:\Program Files\Mozilla Firefox";
            IWebDriver driver = new FirefoxDriver(driverpath);
            driver.Navigate().GoToUrl("https://arrownav.azurewebsites.net");
            driver.FindElement(By.LinkText("Wellness Hub")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/input")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/div/div/form/button")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            String actualOutput = "";
            wait.Until(condition =>
            {
                IWebElement element = driver.FindElement(By.XPath("//*[@id=\"notification\"]"));
                if (element.Text != "")
                {
                    actualOutput = element.Text;
                }
                return element.Text != "";
            });
            var expectedOutput = "Survey Completed";
            Trace.WriteLine(actualOutput);
            driver.Close();
            Assert.IsTrue(actualOutput == expectedOutput);
        }

        [TestMethod]
        public void GenerateRouteShould()
        {
            var driverpath = @"C:\Program Files\Mozilla Firefox";
            FirefoxOptions profile = new FirefoxOptions();
            profile.SetPreference("geo.prompt.testing", true);
            profile.SetPreference("geo.prompt.testing.allow", true);
            IWebDriver driver = new FirefoxDriver(driverpath, profile);
            driver.Navigate().GoToUrl("https://arrownav.azurewebsites.net");
            var window = driver.Manage().Window.Size;
            Actions action = new Actions(driver);
            action.MoveByOffset(window.Width / 2, window.Height / 2).Click().Perform();
            driver.FindElement(By.Id("walking-btn")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            String result = "";
            wait.Until(condition =>
            {
                IWebElement element = driver.FindElement(By.Id("first-route-info"));
                if (element.Text != "")
                {
                    result = element.Text;
                }
                return element.Text != "";
            });
            var actualOutput = result.Split(":")[0];
            var expectedOutput = "Blue Route = Time";
            driver.Close();
            Assert.IsTrue(String.Equals(actualOutput, expectedOutput));
        }
    }
}
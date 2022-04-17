using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using LongHorn.ArrowNav.Managers;
using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Services;
using OpenQA.Selenium.Support.UI;



namespace LongHorn.ArrowNav.Tests
{
    [TestClass]
    public class BuildingCapacityShould
    {
        // write test scenarios for all methods and cases
        [TestMethod]
        public void CapacityManagerShould()
        {
            CapacityManager capacityManager = new CapacityManager();
            var test = capacityManager.GetCapacityValues("TEST");
            var test2 = "";

        }
        public void CapacityServiceShould()
        {

        }

        public void CapacityRepositoryShould()
        {

        }

        public void CapacitySurveyShould()
        {

        }

        public void CapacityPopupShould()
        {
            var driverpath = @"C:\Program Files\Mozilla Firefox";
            FirefoxOptions profile = new FirefoxOptions();
            profile.SetPreference("geo.prompt.testing", true);
            profile.SetPreference("geo.prompt.testing.allow", true);
            IWebDriver driver = new FirefoxDriver(driverpath, profile);
            driver.Navigate().GoToUrl("https://arrownav.azurewebsites.net");
            
            //var actualOutput = result.Split(":")[0];
            //var expectedOutput = "Blue Route = Time";
            //driver.Close();
            //Assert.IsTrue(String.Equals(actualOutput, expectedOutput));
        }


    }
}

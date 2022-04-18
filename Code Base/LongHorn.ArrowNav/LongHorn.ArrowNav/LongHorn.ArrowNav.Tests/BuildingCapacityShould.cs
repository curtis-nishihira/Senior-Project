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
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Services;
using OpenQA.Selenium.Support.UI;



namespace LongHorn.ArrowNav.Tests
{
    [TestClass]
    public class BuildingCapacityShould
    {
        // test scenarios for all methods
        [TestMethod]
        public void CapacityManagerUpdateCapacityValuesShould()
        {
            CapacityManager capacityManager = new CapacityManager();
            CapacitySurveyModel model = new CapacitySurveyModel();
            var test = capacityManager.UpdateCapacityValues(model);
            if (test.Contains("Error")){
                Assert.IsTrue(false);
            }
            else
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void CapacityManagerGetBuildingHoursShould()
        {
            CapacityManager capacityManager = new CapacityManager();
            CapacitySurveyModel model = new CapacitySurveyModel();
            model._Building = "SRWC";
            var test = capacityManager.GetBuildingHours(model);
            if (test.Contains("Error"))
            {
                Assert.IsTrue(false);
            }
            else
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void CapacityServiceUpdateCapacityShould()
        {
            CapacityService capacityService = new CapacityService();
            CapacitySurveyModel model = new CapacitySurveyModel();
            model._Building = "SRWC";
            var test = capacityService.UpdateCapacity(model);
            if (test.Contains("Error"))
            {
                Assert.IsTrue(false);
            }
            else
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void CapacityServiceGetSingleCapacityShould()
        {
            CapacityService capacityService = new CapacityService();
            CapacitySurveyModel model = new CapacitySurveyModel();
            model._Building = "SRWC";
            var test = capacityService.GetSingleCapacity(model);
            if (test._Building.Contains("Error"))
            {
                Assert.IsTrue(false);
            }
            else
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void CapacityServiceGetBuildingHoursShould()
        {
            CapacityService capacityService = new CapacityService();
            CapacitySurveyModel model = new CapacitySurveyModel();
            model._Building = "SRWC";
            var test = capacityService.GetBuildingHours(model);
            if (test.Contains("Error"))
            {
                Assert.IsTrue(false);
            }
            else
            {
                Assert.IsTrue(true);
            }
        }

        // method works in production but file path is augmented by test runner
        //[TestMethod]
        //public void CapacityServiceGetWebLinkShould()
        //{
        //    CapacityService capacityService = new CapacityService();
        //    CapacitySurveyModel model = new CapacitySurveyModel();
        //    model._Building = "SRWC";
        //    var test = capacityService.GetWebLink(model);
        //    if (test.Contains("Error"))
        //    {
        //        Assert.IsTrue(false);
        //    }
        //    else
        //    {
        //        Assert.IsTrue(true);
        //    }
        //}
        //[TestMethod]
        //public void CapacityManagerGetCapacityValuesShould()
        //{
        //    CapacityManager capacityManager = new CapacityManager();
        //    var test = capacityManager.GetCapacityValues("SRWC");
        //    if (test._CapacityValue.Contains("Error"))
        //    {
        //        Assert.IsTrue(true);
        //    }
        //    else
        //    {
        //        Assert.IsTrue(false);
        //    }

        //}


        [TestMethod]
        public void CapacityRepositoryUpdateShould()
        {
            CapacityRepository capacityRepository = new CapacityRepository();
            CapacitySurveyModel model = new CapacitySurveyModel();
            var test = capacityRepository.Update(model);
            if (test.Contains("Error"))
            {
                Assert.IsTrue(false);
            }
            else
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void CapacityRepositoryGetSingleCapacityShould()
        {
            CapacityRepository capacityRepository = new CapacityRepository();
            CapacitySurveyModel model = new CapacitySurveyModel();
            model._Building = "SRWC";
            var test = capacityRepository.GetSingleCapacity(model);
            if (test._WeekdayName.Contains("Error"))
            {
                Assert.IsTrue(false);
            }
            else
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void CapacityRepositoryGetBuildingHoursShould()
        {
            CapacityRepository capacityRepository = new CapacityRepository();
            CapacitySurveyModel model = new CapacitySurveyModel();
            model._Building = "SRWC";
            var test = capacityRepository.GetBuildingHours(model);
            if (test.Contains("Error"))
            {
                Assert.IsTrue(false);
            }
            else
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void CapacitySurveyShould()
        {
            try
            {
                var driverpath = @"C:\Program Files\Mozilla Firefox";
                FirefoxOptions profile = new FirefoxOptions();
                profile.SetPreference("geo.prompt.testing", true);
                profile.SetPreference("geo.prompt.testing.allow", true);
                IWebDriver driver = new FirefoxDriver(driverpath, profile);
                driver.Navigate().GoToUrl("https://arrownav.azurewebsites.net");
                driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div/form/button[1]")).Click();
                driver.FindElement(By.XPath("//*[@id=\"button\"]")).Click();
                driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[5]/div/div/span")).Click();
                driver.Close();
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void CapacityPopupShould()
        {
            try
            {
                var driverpath = @"C:\Program Files\Mozilla Firefox";
                FirefoxOptions profile = new FirefoxOptions();
                profile.SetPreference("geo.prompt.testing", true);
                profile.SetPreference("geo.prompt.testing.allow", true);
                IWebDriver driver = new FirefoxDriver(driverpath, profile);
                driver.Navigate().GoToUrl("https://arrownav.azurewebsites.net");
                driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div/form/button[1]")).Click();
                driver.Close();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }


    }
}

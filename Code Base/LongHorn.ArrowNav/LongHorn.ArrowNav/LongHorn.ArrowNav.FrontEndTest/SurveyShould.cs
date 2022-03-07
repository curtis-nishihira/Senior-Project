using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace LongHorn.ArrowNav.FrontEndTest
{
    [TestClass]
    public class SurveyShould
    {
        [TestMethod]
        public void FillSurvey()
        {
            var driverpath = @"C:\Program Files\Mozilla Firefox";
            IWebDriver driver = new FirefoxDriver(driverpath);
            driver.Navigate().GoToUrl("https://arrownav.azurewebsites.net");
            driver.FindElement(By.LinkText("Wellness Hub")).Click();
            for(int i = 0; i < 10; i++):
                driver.FindElement(By.LinkText("Submit")).Click();
            //driver.Close();
        }
    }
}
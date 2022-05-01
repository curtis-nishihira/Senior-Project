using LongHorn.ArrowNav.Managers;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.Tests
{
    [TestClass]
    public class QuickFindShould
    {
        [TestMethod]
        public void GetCoordsShouldReturnModel()
        {
            //Arrange
            IBuildingService buildingService = new BuildingService();

            BuildingModel model = new BuildingModel();

            // Act
            var actualOutput = buildingService.GetCoordsAsync("JAPANESE GARDEN").Result;

            model = (BuildingModel)actualOutput;

            var expectedBuildingName = "JAPANESE GARDEN";

            // Assert
            Assert.IsTrue(model.buildingName == expectedBuildingName);
        }

        [TestMethod]

        public void GetCoordsShouldReturnEmptyModel()
        {
            // Arrange
            IBuildingService buildingService = new BuildingService();

            BuildingModel model = new BuildingModel();

            // Act
            var actualOutput = buildingService.GetCoordsAsync("GARDEN").Result;

            model = (BuildingModel)actualOutput;

            // Assert
            Assert.IsTrue(actualOutput.GetType() != typeof(string));

            Assert.IsTrue(model.buildingName == "");
        }

        [TestMethod]

        public void GetCoordsShouldReturnValidationError()
        {

            // Arrange
            IBuildingService buildingService = new BuildingService();

            BuildingModel model = new BuildingModel();

            // Act
            var actualOutput = buildingService.GetCoordsAsync(";Select name from database --1").Result;

            string expectedOutput = "Invalid User Input";

            // Assert
            Assert.IsTrue(actualOutput.GetType() == typeof(string));

            Assert.IsTrue((string)actualOutput == expectedOutput);

        }
        [TestMethod]
        public void RetrieveBuildingNameAsyncShouldReturnName()
        {
            // Arrange
            IBuildingService buildingService = new BuildingService();

            // Act
            var actualOutput = buildingService.RetrieveBuildingNameAsync("JG").Result;

            string expectedOutput = "JAPANESE GARDEN";

            // Assert
            Assert.IsTrue(actualOutput == expectedOutput);
        }

        [TestMethod]
        public void RetrieveBuildingNameAsyncShouldReturnNotFound()
        {
            // Arrange
            IBuildingService buildingService = new BuildingService();

            // Act
            var actualOutput = buildingService.RetrieveBuildingNameAsync("JPS").Result;

            string expectedOutput = "Building not Found in Database";

            // Assert
            Assert.IsTrue(actualOutput == expectedOutput);
        }

        [TestMethod]
        public void RetrieveBuildingNameAsyncShouldReturnValidationError()
        {
            // Arrange
            IBuildingService buildingService = new BuildingService();

            // Act
            var actualOutput = buildingService.RetrieveBuildingNameAsync("<WEFWEF").Result;

            string expectedOutput = "Invalid User Input";

            // Assert
            Assert.IsTrue((string)actualOutput == expectedOutput);
        }


        [TestMethod]
        public void RetrieveAcronymAsyncShouldReturnAcronym()
        {
            // Arrange
            IBuildingService buildingService = new BuildingService();

            // Act
            var actualOutput = buildingService.RetrieveAcronymAsync("JAPANESE GARDEN").Result;

            string expectedOutput = "JG";

            // Assert
            Assert.IsTrue((string)actualOutput == expectedOutput);
        }
        [TestMethod]
        public void RetrieveAcronymAsyncShouldReturnValidationError()
        {
            // Arrange
            IBuildingService buildingService = new BuildingService();

            // Act
            var actualOutput = buildingService.RetrieveAcronymAsync("<>LF").Result;

            string expectedOutput = "Invalid User Input";

            // Assert
            Assert.IsTrue((string)actualOutput == expectedOutput);
        }

        [TestMethod]
        public void RetrieveAllBuildingsAsyncShouldReturnList()
        {
            // Arrange
            IBuildingService buildingService = new BuildingService();

            // Act
            var actualOutput = (List<string>)buildingService.RetrieveAllBuildingsAsync().Result;

            // Assert
            Assert.IsTrue(actualOutput.Count > 0);
        }

        [TestMethod]
        public void GetLongLatManagerShouldReturnModel()
        {
            // Arrange
            BuildingManager buildingManager = new BuildingManager();

            // Act
            BuildingModel actualOutput = buildingManager.GetLongLat("JAPANESE GARDEN");

            // Assert
            Assert.IsTrue(actualOutput.longitude != 0.0);

            Assert.IsTrue(actualOutput.latitude != 0.0);
        }

        [TestMethod]
        public void GetLongLatManagerShouldReturnNnotFound()
        {
            // Arrange
            BuildingManager buildingManager = new BuildingManager();

            // Act
            BuildingModel actualOutput = buildingManager.GetLongLat("JAPESE GARDEN");

            var expectedOutput = "Not Found";

            // Assert
            Assert.IsTrue(actualOutput.buildingName == expectedOutput);

        }

        [TestMethod]
        public void GetLongLatManagerShouldReturnInputValidationError()
        {
            // Arrange
            BuildingManager buildingManager = new BuildingManager();

            // Act
            BuildingModel actualOutput = buildingManager.GetLongLat("'sdfs';sd");

            string expectedOutput = "Invalid User Input";

            // Assert
            Assert.IsTrue(actualOutput.buildingName == expectedOutput);

        }

        [TestMethod]
        public void GetBuidingNameManagerShouldReturnBuildingName()
        {
            // Arrange
            BuildingManager buildingManager = new BuildingManager();

            // Act
            string actualOutput = buildingManager.GetBuidingName("LA5");

            string expectedOutput = "LIBERAL ARTS 5";

            // Assert
            Assert.IsTrue(actualOutput == expectedOutput);

        }

        [TestMethod]
        public void GetBuidingNameManagerShouldReturnInputValidcationError()
        {
            // Arrange
            BuildingManager buildingManager = new BuildingManager();

            // Act
            string actualOutput = buildingManager.GetBuidingName("<>;");

            string expectedOutput = "Invalid User Input";

            // Assert
            Assert.IsTrue(actualOutput == expectedOutput);

        }

        [TestMethod]
        public void GetBuidingNameManagerShouldReturnNotFound()
        {
            // Arrange
            BuildingManager buildingManager = new BuildingManager();

            // Act
            string actualOutput = buildingManager.GetBuidingName("");

            string expectedOutput = "Building not Found in Database";

            // Assert
            Assert.IsTrue(actualOutput == expectedOutput);

        }

        [TestMethod]
        public void GetAcronymFromBuildingNameManagerShouldReturnAcronym()
        {
            // Arrange
            BuildingManager buildingManager = new BuildingManager();

            // Act
            string actualOutput = buildingManager.GetAcronymFromBuildingName("JAPANESE GARDEN");

            string expectedOutput = "JG";

            // Assert
            Assert.IsTrue(actualOutput == expectedOutput);

        }

        [TestMethod]
        public void GetAcronymFromBuildingNameManagerShouldReturnNotFound()
        {
            // Arrange
            BuildingManager buildingManager = new BuildingManager();

            // Act
            string actualOutput = buildingManager.GetAcronymFromBuildingName("");

            string expectedOutput = "Not Found";

            // Assert
            Assert.IsTrue(actualOutput == expectedOutput);

        }

        [TestMethod]
        public void GetAcronymFromBuildingNameManagerShouldReturnInputError()
        {
            // Arrange
            BuildingManager buildingManager = new BuildingManager();

            // Act
            string actualOutput = buildingManager.GetAcronymFromBuildingName("<>/");

            string expectedOutput = "Invalid User Input";

            // Assert
            Assert.IsTrue(actualOutput == expectedOutput);

        }

        [TestMethod]
        public void RetrieveAllBuildingsManagerShouldReturnList()
        {
            // Arrange
            BuildingManager buildingManager = new BuildingManager();

            // Act
            List<string> actualOutput = buildingManager.RetrieveAllBuildings();

            // Assert
            Assert.IsTrue(actualOutput.Count > 0);

        }


        //have to have all the new code published to website before I can run tests
        [TestMethod]
        public void SearchBarShould()
        {

            try
            {
                // Arrange
                var pathToMozillaDriver = @"C:\Program Files\Mozilla Firefox";

                FirefoxOptions profile = new FirefoxOptions();

                profile.SetPreference("geo.prompt.testing", true);

                profile.SetPreference("geo.promt.testing.allow", true);

                IWebDriver webDriver = new FirefoxDriver(pathToMozillaDriver, profile);

               // Act 
                webDriver.Navigate().GoToUrl("https://arrownav.azurewebsites.net");

                Thread.Sleep(1000);

                // Finding the button the accept the privacy pop-up
                webDriver.FindElement(By.Id("accept-btn")).Click();

                Thread.Sleep(2500);

                // Inputting a building name that extist in our database
                webDriver.FindElement(By.ClassName("search-bar")).SendKeys("JAPANESE GARDEN");

                Thread.Sleep(1000);

                // Clicking the search button will result in a marker being dropped on the map
                webDriver.FindElement(By.Id("search-btn")).Click();

                Thread.Sleep(2500);

                // Clicking the button to get the fastest walking routes from the users location to the marker
                webDriver.FindElement(By.Id("walking-btn")).Click();

                Thread.Sleep(4000);

                // Clicking the button to get the fastest walking routes from the users location to the marker
                webDriver.FindElement(By.Id("driving-btn")).Click();

                Thread.Sleep(4000);


                // Clicking the button to get the fastest walking routes from the users location to the marker
                webDriver.FindElement(By.Id("cycling-btn")).Click();

                Thread.Sleep(4000);

                webDriver.Close();

            }
            catch
            {

                Assert.IsTrue(false);
               
            }

        }
        
        [TestMethod]
        public void SearchBarShouldAlertNotFound()
        {

            try
            {
                // Arrange
                var pathToMozillaDriver = @"C:\Program Files\Mozilla Firefox";

                FirefoxOptions profile = new FirefoxOptions();

                profile.SetPreference("geo.prompt.testing", true);

                profile.SetPreference("geo.promt.testing.allow", true);

                // Act
                IWebDriver webDriver = new FirefoxDriver(pathToMozillaDriver, profile);

                webDriver.Navigate().GoToUrl("https://arrownav.azurewebsites.net");

                Thread.Sleep(1000);

                // Finding the button the accept the privacy pop-up
                webDriver.FindElement(By.Id("accept-btn")).Click();

                Thread.Sleep(2500);
                
                // Inputting a building name that doesn't extist in our database
                webDriver.FindElement(By.ClassName("search-bar")).SendKeys("GARDEN");

                Thread.Sleep(1000);

                // Clicking the search button which will cause the web app to return an alert due to the building not being found
                webDriver.FindElement(By.Id("search-btn")).Click();

                Thread.Sleep(3000);

                // Saving the text that appeared on the alert to compare to the expected output
                string notFoundAlert = webDriver.SwitchTo().Alert().Text;

                Thread.Sleep(2000);

                // Closes the alert
                webDriver.SwitchTo().Alert().Accept();

                webDriver.Close();

                var expectedOutput = "Not Found. Please try again. Please select an option";

                //Assert
                Assert.IsTrue(notFoundAlert == expectedOutput);

            }
            catch
            {

                Assert.IsTrue(false);
               
            }

        }

        [TestMethod]
        public void SearchBarShouldAlertNothingSelected()
        {

            try
            {
                // Arrange
                var pathToMozillaDriver = @"C:\Program Files\Mozilla Firefox";

                FirefoxOptions profile = new FirefoxOptions();

                profile.SetPreference("geo.prompt.testing", true);

                profile.SetPreference("geo.promt.testing.allow", true);

                // Act 
                IWebDriver webDriver = new FirefoxDriver(pathToMozillaDriver, profile);

                webDriver.Navigate().GoToUrl("https://arrownav.azurewebsites.net");

                Thread.Sleep(1000);

                // Finding the button the accept the privacy pop-up
                webDriver.FindElement(By.Id("accept-btn")).Click();

                Thread.Sleep(2500);

                // Not inputting anything into the search bar
                webDriver.FindElement(By.ClassName("search-bar")).SendKeys("");

                Thread.Sleep(1000);

                // Clicking the search button which will cause the web app to return an alert due to nothing being selected
                // or written
                webDriver.FindElement(By.Id("search-btn")).Click();

                Thread.Sleep(3000);

                // Saving the text that appeared on the alert to compare to the expected output
                string nothingSelctedAlert = webDriver.SwitchTo().Alert().Text;

                Thread.Sleep(2000);

                // Closes the alert
                webDriver.SwitchTo().Alert().Accept();

                webDriver.Close();

                // Act
                var expectedOutput = "Nothing was selected. Please try again";

                //Assert
                Assert.IsTrue(nothingSelctedAlert == expectedOutput);

            }
            catch
            {

                Assert.IsTrue(false);

            }

        }

        [TestMethod]
        public void SearchBarShouldAlertInvalidUserInput()
        {

            try
            {
                // Arrange
                var pathToMozillaDriver = @"C:\Program Files\Mozilla Firefox";

                FirefoxOptions profile = new FirefoxOptions();

                profile.SetPreference("geo.prompt.testing", true);

                profile.SetPreference("geo.promt.testing.allow", true);

                IWebDriver webDriver = new FirefoxDriver(pathToMozillaDriver, profile);

                // Act
                webDriver.Navigate().GoToUrl("https://arrownav.azurewebsites.net");

                Thread.Sleep(1000);
                
                // Finding the button the accept the privacy pop-up
                webDriver.FindElement(By.Id("accept-btn")).Click();

                Thread.Sleep(2500);

                // Inputting invalid characters into the search bar
                webDriver.FindElement(By.ClassName("search-bar")).SendKeys("><?S");

                Thread.Sleep(1000);

                // Clicking the search button which will cause the web app to return an alert due to invalid user input
                webDriver.FindElement(By.Id("search-btn")).Click();

                Thread.Sleep(2500);

                // Saving the text that appeared on the alert to compare to the expected output
                string invalidCharactersAlert = webDriver.SwitchTo().Alert().Text;

                Thread.Sleep(2000);

                // Closes the alert
                webDriver.SwitchTo().Alert().Accept();

                webDriver.Close();

                // Act
                var expectedOutput = "Invalid Charcters were inputted. Please try again.";

                //Assert
                Assert.IsTrue(invalidCharactersAlert == expectedOutput);

            }
            catch
            {

                Assert.IsTrue(false);

            }

        }
    }
}

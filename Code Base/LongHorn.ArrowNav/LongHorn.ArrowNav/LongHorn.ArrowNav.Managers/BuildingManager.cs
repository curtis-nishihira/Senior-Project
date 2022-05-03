using LongHorn.ArrowNav.Logging;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.Managers
{
    public class BuildingManager
    {
        IBuildingService buildingService = new BuildingService();

        DatabaseLogService LogService = new DatabaseLogService();

        Log? logEntry;
        public BuildingModel GetLongLat(string buildingName)
        {
            if(InputValidation(buildingName))
            {
                var response = buildingService.GetCoordsAsync(buildingName).Result;


                BuildingModel building = new BuildingModel();


                if (response.GetType() != typeof(string))
                {

                    building = (BuildingModel)response;

                    if(building.buildingName == "")
                    {
                        building.buildingName = "Not Found";
                    }

                    //try catch ignore on the log call
                    try
                    {
                        logEntry = new Log("BuildingByAcronym Succesfully Called", "Manager", "Info", "User");

                        LogService.Log(logEntry);

                    }
                    catch
                    {

                    }

                    return building;

                }
                else
                {

                    building.buildingName = (string)response;

                    //try catch ignore for the log call
                    try
                    {
                        logEntry = new Log("BuildingByAcronym UnSuccesfully Called", "Manager", "Error", "User");

                        LogService.Log(logEntry);

                    }
                    catch
                    {

                    }

                    return building;

                }
            }
            else
            {
                BuildingModel invalidInput = new BuildingModel();

                invalidInput.buildingName = "Invalid User Input";

                try
                {
                    logEntry = new Log("Invalid Input Detected -> GetLongLat Not Called", "Manager", "Warning", "User");

                    LogService.Log(logEntry);

                }
                catch
                {

                    // Ignoring exceptions being thrown that might prevent business functionality from working

                }

                return invalidInput;
            }
        }
        //doubting a lot here. the if block doesnt seem right
        public string GetBuidingName(string acronym)
        {
            if(InputValidation(acronym))
            {
                var response = buildingService.RetrieveBuildingNameAsync(acronym).Result;

                try
                {
                    logEntry = new Log("GetBuildingName Successfully Called", "Manager", "Info", "User");

                    LogService.Log(logEntry);

                }
                catch
                {

                    // Ignoring exceptions being thrown that might prevent business functionality from working

                }

                return response;

            }
            else
            {
                string invalidInputResponse = "Invalid User Input";

                try
                {
                    logEntry = new Log("Invalid Input Detected -> GetBuildingName Not Called", "Manager", "Warning", "User");

                    LogService.Log(logEntry);

                }
                catch
                {

                    // Ignoring exceptions being thrown that might prevent business functionality from working

                }

                return invalidInputResponse; 

            }

        }
        public string GetAcronymFromBuildingName(string building)
        {
            if(InputValidation(building))
            {

                var response = buildingService.RetrieveAcronymAsync(building).Result;

                if (response == "")
                {

                    response = "Not Found";

                }

                try
                {
                    logEntry = new Log("RetrieveAllBuildings Sucessfully Called", "Manager", "Info", "User");

                    LogService.Log(logEntry);

                }
                catch
                {

                    // Ignoring exceptions being thrown that might prevent business functionality from working

                }

                return response;

            }
            else
            {

                string invalidInputResponse = "Invalid User Input";

                try
                {
                    logEntry = new Log("Invalid Input Detected -> GetBuildingName Not Called", "Manager", "Warning", "User");

                    LogService.Log(logEntry);

                }
                catch
                {

                    // Ignoring exceptions being thrown that might prevent business functionality from working

                }

                return invalidInputResponse;

            }

        }

        public List<string> RetrieveAllBuildings()
        {

            List<string> buildingsCollection = new List<string>();

            var response = buildingService.RetrieveAllBuildingsAsync().Result;

            if(response.GetType() != typeof(string))
            {

                buildingsCollection = (List<string>) response;
                
                try
                {
                    logEntry = new Log("RetrieveAllBuildings Sucessfully Called", "Manager", "Info", "User");

                    LogService.Log(logEntry);

                }
                catch
                {

                    // Ignoring exceptions being thrown that might prevent business functionality from working

                }

                return buildingsCollection;

            }
            else
            {

                try
                {
                    logEntry = new Log("RetrieveAllBuildings UnSucessfully Called", "Manager", "Error", "User");

                    LogService.Log(logEntry);

                }
                catch
                {

                    // Ignoring exceptions being thrown that might prevent business functionality from working

                }

                return buildingsCollection;

            }

            

            

        }

        private bool InputValidation(string userInput)
        {

            bool isValid = false;

            var inputChecker = new Regex("^[a-zA-Z0-9 ]*$");

            if (inputChecker.IsMatch(userInput))
            {

                isValid = true;

            }

            return isValid;

        }

    }
}

using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Logging;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//there should be try catch me on the log calls since it can lead to another problem

namespace LongHorn.ArrowNav.Services
{
    public class BuildingService : IBuildingService
    {
        DatabaseLogService LogService = new DatabaseLogService();

        Log? logEntry;

        IBuildingRepository buildingRepo= new BuildingRepository();

        //Calls a method from the data store and logs where or not it was a successful
        public async Task<string> RetrieveBuildingNameAsync(string acronym)
        {
            string buildingResponse = "";
            
            if (InputValidation(acronym))
            {   

                string? databaseResponse = await Task.Run(() => buildingRepo.BuildingByAcronym(acronym));

                if (databaseResponse == null)
                {
                    buildingResponse = "There was a server error";

                    // Try catch ignore
                    try
                    {
                        logEntry = new Log("BuildingByAcronym UnSuccesfully Called", "Service", "Error", "User");

                        LogService.Log(logEntry);
                    }
                    catch
                    {
                        // Ignoring exceptions being thrown that might prevent business functionality from working
                    }
                }
                else if (databaseResponse == "")
                {
                    buildingResponse = "Unable to find the building in database";

                    // Try catch ignore
                    try
                    {
                        logEntry = new Log("BuildingByAcronym Succesfully Called", "Service", "Info", "User");

                        LogService.Log(logEntry);
                    }
                    catch
                    {
                        // Ignoring exceptions being thrown that might prevent business functionality from working
                    }
                }
                else
                {
                    buildingResponse = databaseResponse;

                    // Try catch ignore
                    try
                    {
                        logEntry = new Log("BuildingByAcronym Succesfully Called", "Service", "Info", "User");

                        LogService.Log(logEntry);
                    }
                    catch
                    {
                        // Ignoring exceptions being thrown that might prevent business functionality from working
                    }
                }

                return buildingResponse;

            }
            else 
            {
                buildingResponse = "Invalid User Input";

                try
                    {
                    logEntry = new Log("Invald Input Detected -> RetrieveBuildingNameAsync Not Called", "Service", "Warning", "User");

                    LogService.Log(logEntry);
                }
                catch
                {
                    // Ignoring exceptions being thrown that might prevent business functionality from working
                }

                return buildingResponse;
            }
            
        }

        //Calls a method from the data store and logs where or not it was a successful
        public async Task<object> GetCoordsAsync(string buildingName)
        {
            if(InputValidation(buildingName))
            {
                BuildingModel? response = await Task.Run(() => buildingRepo.RetrieveBuildingInfo(buildingName));

                if (response.buildingName != null)
                {
                    try
                    {
                        logEntry = new Log("BuildingByAcronym Succesfully Called", "Service", "Info", "User");

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
                    try
                    {
                        logEntry = new Log("BuildingByAcronym UnSuccesfully Called", "Service", "Error", "User");

                        LogService.Log(logEntry);

                    }
                    catch
                    {
                        // Ignoring exceptions being thrown that might prevent business functionality from working
                    }
                    return "Server Error";
                }

            }
            else 
            {

                string invalidInputResponse = "Invalid User Input";

                try
                {
                    logEntry = new Log("Invalid Input Detected -> GetCoordsAsync Not Called", "Service", "Error", "User");

                    LogService.Log(logEntry);

                }
                catch
                {
                    // Ignoring exceptions being thrown that might prevent business functionality from working
                }

                return invalidInputResponse;

            }
        }

        //Calls a method from the data store and logs where or not it was a successful
        public async Task<string> RetrieveAcronymAsync(string buildingName)
        {
            if(InputValidation(buildingName))
            {
                string? response = await Task.Run(() => buildingRepo.AcryonmByBuilding(buildingName));

                if (response == null)
                {

                    try
                    {

                        logEntry = new Log("AcryonmByBuilding UnSuccessfully Called", "Service", "Error", "User");

                        LogService.Log(logEntry);

                    }
                    catch
                    {

                        // Ignoring exceptions being thrown that might prevent business functionality from working
                    
                    }

                    return "Server error";

                }
                else
                {

                    try
                    {

                        logEntry = new Log("RetrieveAcronymAsync Successfully Called", "Service", "Info", "User");

                        LogService.Log(logEntry);

                    }
                    catch
                    {

                        // Ignoring exceptions being thrown that might prevent business functionality from working
                    
                    }

                    return response;

                }
            }
            else
            {
                string invalidInputResponse = "Invalid User Input";

                try
                {
                    logEntry = new Log("Invalid Input Detected -> RetrieveAcronymAsync Not Called", "Service", "Warning", "User");

                    LogService.Log(logEntry);

                }
                catch
                {

                    // Ignoring exceptions being thrown that might prevent business functionality from working
                
                }

                return invalidInputResponse;
            
            }

        }

        //Calls a method from the data store and logs where or not it was a successful
        public async Task<object> RetrieveAllBuildingsAsync()
        {
            List<string>? response = await Task.Run(() => buildingRepo.RetrieveAllBuildings());

            if (response == null)
            {

                try
                {

                    logEntry = new Log("RetrieveAllBuildingsAsync UnSuccessfully Called", "Service", "Error", "User");

                    LogService.Log(logEntry);

                }
                catch 
                {

                    // Ignoring exceptions being thrown that might prevent business functionality from working

                }

                return "Server error";

            }
            else
            {

                try
                {

                    logEntry = new Log("RetrieveAllBuildingsAsync Successfully Called", "Service", "Error", "User");

                    LogService.Log(logEntry);

                }
                catch 
                {

                    // Ignoring exceptions being thrown that might prevent business functionality from working

                }

                return response;

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

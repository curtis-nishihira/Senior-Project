using Microsoft.AspNetCore.Mvc;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Managers;
using System.Text.Json;
using LongHorn.ArrowNav.DAL;
using System.Text.RegularExpressions;

namespace Front_End.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BuildingController : ControllerBase
    {
        LogManager logManager = new LogManager();

        Log? logEntry;

        BuildingManager buildingManager = new BuildingManager();

        [HttpGet]
        [Route("getBuildingbyAcronym")]
        public string GetBuildingByAcronym(string acronym)
        {
            if (InputValidation(acronym))
            {

                string response = buildingManager.GetBuidingName(acronym);

                // Try catch ignore
                try
                {
                    logEntry = new Log("GetBuildingName Succesfully Called", "Controller", "Info", "User");

                    logManager.Log(logEntry);

                }
                catch
                {
                    // Ignoring exceptions being thrown that might prevent business functionality from working
                }

                return response;

            }
            else
            {

                // Try catch ignore
                try
                {
                    logEntry = new Log("Invalid User Input", "Controller", "Warning", "User");

                    logManager.Log(logEntry);

                }
                catch
                {
                    // Ignoring exceptions being thrown that might prevent business functionality from working
                }

                return "Error: Invalid Characters were detected";
            }
        }
        [HttpGet]
        [Route("getAllBuildings")]
        public List<string> GetAllBuildings()
        {
            List<string> buildingsCollection = buildingManager.RetrieveAllBuildings();

            // Try catch ignore
            try
            {
                logEntry = new Log("RetrieveAllBuildings Succesfully Called", "Controller", "Info", "User");

                logManager.Log(logEntry);

            }
            catch
            {
                // Ignoring exceptions being thrown that might prevent business functionality from working
            }

            return buildingsCollection;
        }
        [HttpGet]
        [Route("getAcronymbyBuildingName")]
        public string GetAcronymbyBuilding(string buildingName)
        {
            if(InputValidation(buildingName))
            {

                string response = buildingManager.GetAcronymFromBuildingName(buildingName);

                // Try catch ignore
                try
                {
                    logEntry = new Log("GetAcronymFromBuildingName Succesfully Called", "Controller", "Info", "User");

                    logManager.Log(logEntry);

                }
                catch
                {
                    // Ignoring exceptions being thrown that might prevent business functionality from working
                }

                return response;
            
            }
            else
            {

                // Try catch ignore
                try
                {
                    logEntry = new Log("Invalid User Input", "Controller", "Warning", "User");

                    logManager.Log(logEntry);

                }
                catch
                {
                    // Ignoring exceptions being thrown that might prevent business functionality from working
                }

                return "Error: Invalid Characters were detected";
            }
        }
        
        [HttpPost]
        [Route("getLatLong")]
        public BuildingModel GetLatLong(string BuildingName)
        {
            if (InputValidation(BuildingName))
            {
                BuildingModel buildingInfo = buildingManager.GetLongLat(BuildingName);

                // Try catch ignore
                try
                {
                    logEntry = new Log("GetLatLong Succesfully Called", "Controller", "Info", "User");
                    
                    logManager.Log(logEntry);

                }
                catch
                {
                    // Ignoring exceptions being thrown that might prevent business functionality from working
                }

                return buildingInfo;
            }
            else
            {
                BuildingModel buildingModel = new BuildingModel();

                buildingModel.buildingName = "Error: Invalid Characters were detected";

                // Try catch ignore
                try
                {
                    logEntry = new Log("Invalid User Input", "Controller", "Warning", "User");

                    logManager.Log(logEntry);

                }
                catch
                {
                    // Ignoring exceptions being thrown that might prevent business functionality from working
                }

                return buildingModel;
            }

        }

        private bool InputValidation(string input)
        {
            bool isValid = false;

            var inputChecker = new Regex("^[a-zA-Z0-9 ]*$");

            if (inputChecker.IsMatch(input))
            {

                isValid = true;
            
            }

            return isValid;
        }
    }
}
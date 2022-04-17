using Microsoft.AspNetCore.Mvc;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Managers;
using System.Text.Json;
using LongHorn.ArrowNav.Logging;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CapacityController : ControllerBase
    {
        LogManager logManager = new LogManager();

        [HttpPost]
        [Route("getCapacity")]
        public CapacityPopupModel GetCapacity(string BuildingName)
        {
            Log entry;
            CapacityManager CapacityManager = new CapacityManager(); ;
            CapacityPopupModel response = CapacityManager.GetCapacityValues(BuildingName);
            if (response == null || response._CapacityValue.Contains("Error"))
            {
                CapacityPopupModel error = new CapacityPopupModel();
                error._CapacityValue = "Error: Capacity Controller Failure -> GetCapacity()";
                entry = new Log("GetCapacity Unsucessfully Called", "Controller", "Error", "User");
                logManager.Log(entry);
                return error;
            }
            entry = new Log("GetCapacity Sucessfully Called", "Controller", "Info", "User");
            logManager.Log(entry);
            return response;
        }

        // dont know what im gonna do yet for time
        [HttpPost]
        [Route("updateCapacity")]
        public string UpdateCapacity(List<CapacitySurveyModel> list)
        {
            var returnString = "";
            Log entry;
            // updates each of the buildings from the survey
            for (int i = 0; i < list.Count; i++)
            {
                CapacitySurveyModel model = list[i];
                CapacityManager capacityManager = new CapacityManager(); ;
                // only perform update if the value is not n/a -> 0
                if(model._AddValue != 0)
                {
                    var response = capacityManager.UpdateCapacityValues(model);

                    if (response == null || response.Contains("Error"))
                    {
                        entry = new Log("UpdateCapacity Unsucessfully Called", "Controller", "Error", "User");
                        logManager.Log(entry);
                        return "Error: Capacity Controller Failure -> UpdateCapacity()";
                    }
                }
                returnString = "updated successfuly";
            }

            entry = new Log("UpdateCapacity Sucessfully Called", "Controller", "Info", "User");
            logManager.Log(entry);
            return returnString;
        }
        // seperate output by a dash
        [HttpPost]
        [Route("getBuildingHours")]
        public string getBuildingHours(CapacitySurveyModel model)
        {
            CapacityManager capacityManager = new CapacityManager();
            var response = capacityManager.GetBuildingHours(model);
            Log entry;

            if (response == null || response.Contains("Error"))
            {
                entry = new Log("GetBuildingHours Unsucessfully Called", "Controller", "Error", "User");
                logManager.Log(entry);
                return "Error: Capacity Controller Failure -> GetBuildingHours()";
            }
            entry = new Log("GetBuildingHours Sucessfully Called", "Controller", "Info", "User");
            logManager.Log(entry);
            return response;
        }

    }
}
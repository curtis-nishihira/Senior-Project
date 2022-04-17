using Microsoft.AspNetCore.Mvc;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Managers;
using System.Text.Json;
using LongHorn.ArrowNav.DAL;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CapacityController : ControllerBase
    {
        [HttpPost]
        [Route("getCapacity")]
        public string GetCapacity(string BuildingName)
        {
            CapacityManager CapacityManager = new CapacityManager(); ;
            var response = CapacityManager.GetCapacityValues(BuildingName);
            return response;
        }

        // dont know what im gonna do yet for time
        [HttpPost]
        [Route("updateCapacity")]
        public string UpdateCapacity(List<CapacitySurveyModel> list)
        {
            var returnString = "";
            for (int i = 0; i < list.Count; i++)
            {
                CapacitySurveyModel model = list[i];
                CapacityManager capacityManager = new CapacityManager(); ;
                var response = capacityManager.UpdateCapacityValues(model);
                returnString = "updated successfuly";
            }
            return returnString;
        }
        // seperate output by a dash
        [HttpPost]
        [Route("getBuildingHours")]
        public string getBuildingHours(CapacitySurveyModel model)
        {
            CapacityManager capacityManager = new CapacityManager();
            var response = capacityManager.GetBuildingHours(model);
            return response;
        }

    }
}
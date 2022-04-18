using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Services;
using LongHorn.ArrowNav.Logging;

namespace LongHorn.ArrowNav.Managers
{
    public class CapacityManager
    {
        DatabaseLogService databaseLogService = new DatabaseLogService();

        public string UpdateCapacityValues(CapacitySurveyModel model)
        {
            CapacityService CapacityService = new CapacityService();
            var response = CapacityService.UpdateCapacity(model);
            Log entry;

            if (response == null || response.Contains("Error"))
            {
                entry = new Log("Update Capacity Values Unsucessfully Called", "Service", "Error", "User");
                databaseLogService.Log(entry);
                return "Error: Capacity Manager Failure -> UpdateCapacityValues()";
            }

            entry = new Log("Update Capacity Values Sucessfully Called", "Manager", "Info", "User");
            databaseLogService.Log(entry);
            return response;
        }

        // Building name is the acronym for the building
        public CapacityPopupModel GetCapacityValues(string buildingName)
        {
            CapacityService CapacityService = new CapacityService();
            CapacitySurveyModel model = new CapacitySurveyModel();
            CapacityPopupModel returnModel = new CapacityPopupModel();
            Log entry;

            // model building value used for following var defs
            model._Building = buildingName;

            var response = CapacityService.GetSingleCapacity(model);

            if (response == null || response._Building.Contains("Error"))
            {
                CapacityPopupModel error = new CapacityPopupModel();
                error._CapacityValue = "Error: Capacity Manager Failure -> GetCapacityValues()";
                entry = new Log("GetCapacityValues Unsucessfully Called", "Manager", "Error", "User");
                databaseLogService.Log(entry);
                return error;
            }

            var webLink = CapacityService.GetWebLink(model);

            // returns a string of format "HH:MM-HH:MM"
            var hours = CapacityService.GetBuildingHours(model);
            returnModel._Time = hours;

            
            // need to trim off the last 3 characters time has too many values
            
            returnModel._WebLink = webLink;

            // If the objects comes back empty it means there are no hours 
            if (hours == "Closed")
            {
                returnModel._CapacityValue = "Closed";
            }
            if (response._TotalSurveys < 100) // use default until have 100+ surveys
            {
                returnModel._CapacityValue = response._DefaultValue.ToString() + "/5";
            }
            else // take the avg of all survey data 
            {
                int avg = response._TotalValue / response._TotalSurveys;
                returnModel._CapacityValue = avg.ToString() + "/5";
            }

            entry = new Log("GetCapacityValues Sucessfully Called", "Manager", "Info", "User");
            databaseLogService.Log(entry);
            return returnModel;
        }

        public string GetBuildingHours(CapacitySurveyModel model)
        {
            CapacityService capacityService = new CapacityService();
            var response = capacityService.GetBuildingHours(model);
            Log entry;

            if (response == null || response.Contains("Error"))
            {
                entry = new Log("GetBuildingHours Unsucessfully Called", "Manager", "Error", "User");
                databaseLogService.Log(entry);
                return "Error: Capacity Manager Failure -> GetBuildingHours()";
            }

            entry = new Log("GetBuildingHours Sucessfully Called", "Manager", "Info", "User");
            databaseLogService.Log(entry);
            if (response == "")
            {
                return "Closed";
            }
            return response;
        }
    }
}

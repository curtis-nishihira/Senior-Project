using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Services;

namespace LongHorn.ArrowNav.Managers
{
    public class CapacityManager
    {
        public string UpdateCapacityValues(CapacitySurveyModel model)
        {
            CapacityService CapacityService = new CapacityService();
            var response = CapacityService.UpdateCapacity(model);
            return response;
        }

        public string GetCapacityValues(string buildingName)
        {
            CapacityService CapacityService = new CapacityService();
            CapacitySurveyModel model = new CapacitySurveyModel();
            model._Building = buildingName;
            var response = CapacityService.GetSingleCapacity(model);
            if (response._Building == "")
            {
                 return "Closed";
            }
            if (response._TotalSurveys < 100)
            {
                return response._DefaultValue.ToString() + "/5";
            }
            int avg = response._TotalValue / response._TotalSurveys;
            return avg.ToString() + "/5";
        }

        public string GetBuildingHours(CapacitySurveyModel model)
        {
            CapacityService capacityService = new CapacityService();
            var response = capacityService.GetBuildingHours(model);
            if (response == "")
            {
                return "Closed";
            }
            return response;
        }
    }
}

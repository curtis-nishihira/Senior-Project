using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;

namespace LongHorn.ArrowNav.Services
{
    public class CapacityService : ICapacityService
    {
        public string UpdateCapacity(CapacitySurveyModel model)
        {
            CapacityRepository capacityRepo = new CapacityRepository();
            var response = capacityRepo.Update(model);
            return response;
        }

        public CapacityModel GetSingleCapacity(CapacitySurveyModel model)
        {
            CapacityRepository capacityRepo = new CapacityRepository();
            model._TimeSlot = DateTime.Now.ToString("HH:00");
            model._WeekdayName = DateTime.Now.DayOfWeek.ToString();
            CapacityModel response = capacityRepo.getSingleCapacity(model);
            return response;
        
        }

        public string GetBuildingHours(CapacitySurveyModel model)
        {
            CapacityRepository capacityRepo = new CapacityRepository();
            model._WeekdayName = DateTime.Now.DayOfWeek.ToString();
            var response = capacityRepo.getBuildingHours(model);
            return response;
        }
    }
}

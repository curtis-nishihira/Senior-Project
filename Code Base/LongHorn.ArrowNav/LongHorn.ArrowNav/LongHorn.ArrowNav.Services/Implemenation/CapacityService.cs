using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Logging;
using LongHorn.ArrowNav.Models;


namespace LongHorn.ArrowNav.Services
{
    public class CapacityService : ICapacityService
    {
        DatabaseLogService databaseLogService = new DatabaseLogService();

        public string UpdateCapacity(CapacitySurveyModel model)
        {
            CapacityRepository capacityRepo = new CapacityRepository();
            var response = capacityRepo.Update(model);
            Log entry;

            if (response == null)
            {
                entry = new Log("Update Capacity Unsucessfully Called", "Service", "Error", "User");
                databaseLogService.Log(entry);
                return "Error: Capacity Service Failure -> UpdateCapacity()";
            }

            entry = new Log("Update Capacity Sucessfully Called", "Service", "Info", "User");
            databaseLogService.Log(entry);
            return response;
        }

        public CapacityModel GetSingleCapacity(CapacitySurveyModel model)
        {
            CapacityRepository capacityRepo = new CapacityRepository();
            model._TimeSlot = DateTime.Now.ToString("HH:00");
            model._WeekdayName = DateTime.Now.DayOfWeek.ToString();
            CapacityModel response = capacityRepo.GetSingleCapacity(model);
            Log entry;

            if (response == null)
            {
                CapacityModel error = new CapacityModel();
                error._Building = "Error: Capacity Service Failure -> GetSingleCapacity()";
                entry = new Log("GetSingleCapacity Unsucessfully Called", "Service", "Error", "User");
                databaseLogService.Log(entry);
                return error;
            }

            entry = new Log("GetSingleCapacity Sucessfully Called", "Service", "Info", "User");
            databaseLogService.Log(entry);
            return response;
        
        }

        public string GetBuildingHours(CapacitySurveyModel model)
        {
            CapacityRepository capacityRepo = new CapacityRepository();
            model._WeekdayName = DateTime.Now.DayOfWeek.ToString();
            var response = capacityRepo.GetBuildingHours(model);

            // the string wont come back empty because of time dash but essentially looking for empty strings
            if (response != "-")
            {
                string startTime = response.Split('-')[0].Substring(0, response.Split('-')[0].Length - 3);
                string endTime = response.Split('-')[1].Substring(0, response.Split('-')[0].Length - 3);
                response = startTime + " - " + endTime;
            }
            else if(response == "-")
            {
                response = "Closed";
            }

            Log entry;

            if (response == null)
            {
                entry = new Log("GetBuildingHours Unsucessfully Called", "Service", "Error", "User");
                databaseLogService.Log(entry);
                return "Error: Capacity Service Failure -> GetBuildingHours()";
            }

            entry = new Log("GetBuildingHours Sucessfully Called", "Service", "Info", "User");
            databaseLogService.Log(entry);
            return response;
        }

        // only 3 values could be moved to database later but for now will be on txt file for more optimal storage
        public string GetWebLink(CapacitySurveyModel model)
        {
            var weblinks = ConfigurationManager.AppSettings.Get("CapacityWebLinks");
            using (WebClient client = new WebClient())
            {
                string weblinkString = client.DownloadString(weblinks);
                string[] text = weblinkString.Split("\r\n");
                if (model._Building == "SRWC")
                {
                    return text[0];
                }
                else if (model._Building == "LIB")
                {
                    return text[1];
                }
                else if (model._Building == "USU")
                {
                    return text[2];
                }
                else return "";
            }
            
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Managers;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrafficSurveyController : ControllerBase
    {
        [HttpGet]
        public Dictionary<String, Tuple<int, int>> GetZoneValues()
        {
            //Dictionary<String, int> trafficValues = new Dictionary<String, int>();
            TrafficModel model = new TrafficModel();
            DayOfWeek dayOfWeek = DateTime.UtcNow.DayOfWeek;
            //model._WeekdayName = dayOfWeek.ToString();
            //model._TimeSlot = DateTime.UtcNow.ToString("HH:00");
            model._WeekdayName = "Monday";
            model._TimeSlot = "9:00";
            TrafficManager trafficManager = new TrafficManager(); ;
            var response = trafficManager.GetZonevalues(model);
            return response;
        }


        [HttpPost]
        public string Update(List<TrafficModel> list)
        {
            var returnString = "";
            for (int i = 0; i < list.Count; i++)
            {
                TrafficModel model = list[i];
                DayOfWeek dayOfWeek = DateTime.UtcNow.DayOfWeek;
                //model._WeekdayName = dayOfWeek.ToString();
                model._WeekdayName = "Monday";
                //model._TimeSlot = DateTime.UtcNow.ToString("HH:00");
                model._TimeSlot = "9:00";
                TrafficManager trafficManager = new TrafficManager(); ;
                var response  = trafficManager.UpdateZoneValues(model);
                returnString = returnString + " " + response;
                returnString = returnString + model._ZoneName + " " + model._WeekdayName + " " + model._TimeSlot + " " + model._TotalValue + " " + "\n";

            }
            return returnString;
        }

    }
}
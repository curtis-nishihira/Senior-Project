using Microsoft.AspNetCore.Mvc;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Managers;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrafficSurveyController : ControllerBase
    {
        [HttpPost]
        public string Login(List<TrafficModel> list)
        {
            var returnString = "";
            for (int i = 0; i < list.Count; i++)
            {
                TrafficModel model = list[i];
                DayOfWeek dayOfWeek = DateTime.UtcNow.DayOfWeek;
                model._WeekdayName = dayOfWeek.ToString();
                model._TimeSlot = DateTime.UtcNow.ToString("HH:00");
                TrafficManager trafficManager = new TrafficManager(); ;
                var response  = trafficManager.UpdateZoneValues(model);
                returnString = returnString + " " + response;

                returnString = returnString + model._ZoneName + " " + model._WeekdayName + " " + model._TimeSlot + " " + model._TotalValue + " " + "\n";

            }
            return returnString;
        }



    }
}
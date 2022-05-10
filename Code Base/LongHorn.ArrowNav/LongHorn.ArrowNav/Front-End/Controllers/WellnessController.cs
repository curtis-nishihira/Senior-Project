using Microsoft.AspNetCore.Mvc;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Managers;
using LongHorn.ArrowNav.DAL;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WellnessController: ControllerBase
    {
        [HttpPost]
        [Route("setForm")]
        public string SetForm(StudentWellnessModel form)
        {
            WellnessRepository repo = new WellnessRepository();
            var result = repo.Create(form);
            return result;
        }
        
        [HttpGet]
        [Route("getWaterIntake")]
        public double GetWaterIntake(string Username)
        {
            WellnessRepository repo = new WellnessRepository();
            var bodyWeight = repo.GetWaterIntake(Username);
            return bodyWeight;
        }

        [HttpGet]
        [Route("getBodyWeight")]
        public int GetBodyWeight(string Username)
        {
            WellnessRepository repo = new WellnessRepository();
            var bodyWeight = repo.GetBodyWeight(Username);
            return bodyWeight;
        }

        [HttpGet]
        [Route("getStartTime")]
        public string GetStartTime(string Username)
        {
            WellnessRepository repo = new WellnessRepository();
            var startTime = repo.GetStartTime(Username);
            return startTime;
        }

        [HttpGet]
        [Route("getEndTime")]
        public string GetEndTime(string Username)
        {
            WellnessRepository repo = new WellnessRepository();
            var endTime = repo.GetEndTime(Username);
            return endTime;
        }

        [HttpPost]
        [Route("setBodyWeight")]
        public string SetBodyWeight(StudentWellnessModel Username)
        {
            WellnessRepository repo = new WellnessRepository();
            var bodyWeight = repo.SetBodyWeight(Username);
            return bodyWeight;
        }

        [HttpPost]
        [Route("setStartTime")]
        public string SetStartTime(StudentWellnessModel Username)
        {
            WellnessRepository repo = new WellnessRepository();
            var startTime =repo.SetStartTime(Username);
            return startTime;
        }

        [HttpPost]
        [Route("setEndTime")]
        public string SetEndTime(StudentWellnessModel Username)
        {
            WellnessRepository repo = new WellnessRepository();
            var endTime = repo.SetEndTime(Username);
            return endTime;
        }
        [HttpGet]
        [Route("getReminder")]
        public bool GetReminder(string Username)
        {
            try
            {
                WellnessRepository repo = new WellnessRepository();
                var endTime = repo.GetReminder(Username);
                return endTime;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}

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
        public string setForm(StudentWellnessModel form)
        {
            WellnessRepository repo = new WellnessRepository();
            var result = repo.Create(form);
            return result;
        }

        [HttpGet]
        [Route("getBodyWeight")]
        public int getBodyWeight(string Username)
        {
            WellnessRepository repo = new WellnessRepository();
            var bodyWeight = repo.GetBodyWeight(Username);
            return bodyWeight;
        }

        [HttpGet]
        [Route("getStartTime")]
        public string getStartTime(string Username)
        {
            WellnessRepository repo = new WellnessRepository();
            var startTime = repo.GetStartTime(Username);
            return startTime;
        }

        [HttpGet]
        [Route("getEndTime")]
        public string getEndTime(string Username)
        {
            WellnessRepository repo = new WellnessRepository();
            var endTime = repo.GetEndTime(Username);
            return endTime;
        }

        [HttpPost]
        [Route("setBodyWeight")]
        public string setBodyWeight(StudentWellnessModel Username)
        {
            WellnessRepository repo = new WellnessRepository();
            var bodyWeight = repo.SetBodyWeight(Username);
            return bodyWeight;
        }

        [HttpPost]
        [Route("setStartTime")]
        public string setStartTime(StudentWellnessModel Username)
        {
            WellnessRepository repo = new WellnessRepository();
            var startTime =repo.SetStartTime(Username);
            return startTime;
        }

        [HttpPost]
        [Route("setEndTime")]
        public string setEndTime(StudentWellnessModel Username)
        {
            WellnessRepository repo = new WellnessRepository();
            var endTime = repo.SetEndTime(Username);
            return endTime;
        }
    }
}

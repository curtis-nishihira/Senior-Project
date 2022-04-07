using Microsoft.AspNetCore.Mvc;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Managers;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
        [HttpGet]
        public string get()
        {
            return "default get";
        }
        [HttpPost]
        [Route("scheduleadd")]
        public string AddClass(StudentClassModel studentclass)
        {
            ScheduleManager scheduleManager = new ScheduleManager();
            var result = scheduleManager.Create(studentclass);

            return "";
        }
    }
}

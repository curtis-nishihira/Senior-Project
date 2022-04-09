using Microsoft.AspNetCore.Mvc;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Managers;
using LongHorn.ArrowNav.DAL.Implementation;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
        [HttpGet]
        [Route("getschedule")]
        public List<StudentClassModel> GetScheduleByUsername(string email)
        {
            ScheduleRepository repo = new ScheduleRepository();
            List<StudentClassModel> classes = repo.Read(email);
            return classes;
        }

        [HttpPost]
        [Route("scheduleadd")]
        public string AddClass(StudentClassModel studentclass)
        {
            ScheduleManager scheduleManager = new ScheduleManager();
            var result = scheduleManager.Create(studentclass);

            return result;
        }
        [HttpPost]
        [Route("scheduledelete")]
        public string DeleteClass(StudentClassModel studentclass)
        {
            ScheduleManager scheduleManager = new ScheduleManager();
            var result = scheduleManager.Delete(studentclass);
            return result;
        }
        [HttpPost]
        [Route("scheduleedit")]
        public string EditClass(StudentClassModel studentclass)
        {
            ScheduleManager scheduleManager= new ScheduleManager();
            var result = scheduleManager.Edit(studentclass);
            return result;
        }
    }
}
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.Managers
{
    public class ScheduleManager
    {
        public ScheduleManager()
        {

        }
        public string Create(StudentClassModel studentclass)
        {
            CreateService createService = new CreateService();
            var result = createService.CreateStudentClass(studentclass);
            Log entry = new Log(result, "Info", "Data Layer", studentclass._Username);
            LogManager logManager = new LogManager();
            logManager.Log(entry);
            return result;
        }

        public string Delete(StudentClassModel studentclass)
        {
            DeleteService deleteService = new DeleteService();
            var result = deleteService.DeleteStudentClass(studentclass);
            Log entry = new Log(result, "Info", "Data Layer", studentclass._Username);
            LogManager logManager = new LogManager();
            logManager.Log(entry);
            return result;
        }

        public string Edit(StudentClassModel studentclass)
        {
            UpdateService updateService = new UpdateService();
            var result = updateService.EditStudentClass(studentclass);
            Log entry = new Log(result, "Info", "Data Layer", studentclass._Username);
            LogManager logManager = new LogManager();
            logManager.Log(entry);
            return result;
        }
    }
}
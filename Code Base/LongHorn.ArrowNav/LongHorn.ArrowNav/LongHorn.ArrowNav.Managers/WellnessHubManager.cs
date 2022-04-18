using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.Managers
{
    public class WellnessHubManager
    {
        public WellnessHubManager()
        {

        }

        public string Create(StudentWellnessModel studentWellnessModel)
        {
            CreateService createService = new CreateService();
            var result = createService.CreateWellnessReminder(studentWellnessModel);
            Log entry = new Log(result, "Info", "Data Layer", studentWellnessModel._Username);
            LogManager logManager = new LogManager();
            logManager.Log(entry);
            return result;
        }
        public string Delete(StudentWellnessModel studentWellnessModel)
        {
            DeleteService deleteService = new DeleteService();
            var result = deleteService.DeleteWellnessReminder(studentWellnessModel);
            Log entry = new Log(result, "Info", "Data Layer", studentWellnessModel._Username);
            LogManager logManager = new LogManager();
            logManager.Log(entry);
            return result;
        }
        public string Edit(StudentWellnessModel studentWellnessModel)
        {
            UpdateService editService = new UpdateService();
            var result = editService.EditWellnessReminder(studentWellnessModel);
            Log entry = new Log(result, "Info", "Data Layer", studentWellnessModel._Username);
            LogManager logManager = new LogManager();
            logManager.Log(entry);
            return result;
        }
    }
}

using LongHorn.Logging;
using LongHorn.ArrowNav.Models;
using System;

namespace LongHorn.ArrowNav.Managers
{
    public class LogManager
    {

        public LogManager()
        {
        }
        public string Log(Log logEntry)
        {

            DatabaseLogService logService = new DatabaseLogService(); //<-Annoying. Can't run the timer methods if I use IlogService logService = new DatabaseLogService()
            logService.TimerStart();
            var result = logService.Log(logEntry);
            logService.TimerEnd();
            if (logService.TimerCheck())
            {
                return result;
            }
            else
            {
                return "Logging process took longer than 5 seconds";
            }
        }
    }
}

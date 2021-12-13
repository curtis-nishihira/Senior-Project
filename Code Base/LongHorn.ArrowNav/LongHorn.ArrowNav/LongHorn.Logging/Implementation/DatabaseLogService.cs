using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using System;
using System.Diagnostics;

namespace LongHorn.Logging
{
    public class DatabaseLogService : ILogService
    {
        public Stopwatch _watch = new Stopwatch();
        public string Log(Log entry)
        {
            IRepository<Log> loggingRepository = new LoggingRepository();
            var result = loggingRepository.Create(entry);
            return result;
        }
        public void TimerStart()
        {
            _watch.Start();
        }

        public void TimerEnd()
        {
            _watch.Stop();
        }

        public bool TimerCheck()
        {
            var totalSeconds = _watch.Elapsed.TotalSeconds;
            if (totalSeconds <= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

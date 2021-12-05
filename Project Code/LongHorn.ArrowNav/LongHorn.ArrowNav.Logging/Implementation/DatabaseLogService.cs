using LongHorn.ArrowNav.DAL;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LongHorn.Logging
{
    public class DatabaseLogService : ILogService
    {
        private double timeElapsed;
        public bool Log(string description)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            IRepository<string> r = new LoggingRepository();
            var result = r.Create(description);
            watch.Stop();
            timeElapsed = watch.Elapsed.TotalSeconds;
            return result;
        
        }

        public double getElapsedTime()
        {
            return timeElapsed;
        }
    }

}
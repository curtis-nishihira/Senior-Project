﻿using LongHorn.DAL;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LongHorn.ArrowNav.Logging
{
    public class DatabaseLogService : ILogService
    {
        private double timeElapsed;
        public bool Log(string description)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            IRepository<string> r = new SqlDAO();
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
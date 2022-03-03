using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.Logging
{
    public class InMemoryLogService : ILogService
    {
        private readonly IList<Log> _logStore;

        public InMemoryLogService()
        {
            _logStore = new List<Log>();
        }

        public IList<Log> GetAllLogs()
        {
            return _logStore;
        }

        public string Log(Log entry)
        {
            try
            {
                _logStore.Add(entry);
                return "Successful Log";
            }
            catch 
            {
                return "Not logged successfully";
            }
        }
    }
}

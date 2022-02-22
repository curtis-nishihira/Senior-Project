using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Logging
{
    public interface ILogService
    {
        string Log(Log entry);

        IList<Log> GetAllLogs();
    }
}

using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.Logging
{
    public interface ILogService
    {
        string Log(Log entry);
    }
}

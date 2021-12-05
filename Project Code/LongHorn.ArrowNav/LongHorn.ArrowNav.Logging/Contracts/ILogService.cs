using LongHorn.ArrowNav.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.Logging
{
    public interface ILogService
    {
        bool Log(string description);
        double getElapsedTime();
    }
}

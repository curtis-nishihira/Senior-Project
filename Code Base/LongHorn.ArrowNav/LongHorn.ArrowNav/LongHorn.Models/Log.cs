using System;

namespace LongHorn.Models
{
    public class Log
    {
        public string _Log { get; }
        public string _Level { get; }
        public string _Type { get; }

        public string _User { get; }
        public DateTime _UtcTime { get; }

        public Log(string description, string level, string type, string user, DateTime time)
        {
            _Log = description;
            _Level = level;
            _Type = type;
            _UtcTime = time;
            _User = user;
        }
    }
}

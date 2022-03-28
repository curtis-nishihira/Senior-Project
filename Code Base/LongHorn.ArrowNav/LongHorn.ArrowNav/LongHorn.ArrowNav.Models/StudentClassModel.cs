using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.Models
{
    public class StudentClassModel
    {
        public string _subject { get; set; } = "";
        public string _course { get; set; } = "";
        public int _section { get; set; } = 0;
        public string _building { get; set; } = "";
        public string _room { get; set; } = "";
        public string _day { get; set; } = "";
        public string _secondDay { get; set; } = "";
        public string _startTime { get; set; } = "";
        public string _endTime { get; set; } = "";
    }
}

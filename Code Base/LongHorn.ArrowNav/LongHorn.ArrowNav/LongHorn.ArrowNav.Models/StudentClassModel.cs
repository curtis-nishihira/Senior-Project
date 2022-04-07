using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.Models
{
    public class StudentClassModel
    {
        public string _Username { get; set; } = "";
        public string _course { get; set; } = "";
        
        public string _coursetype { get; set; } = "";
        public string _building { get; set; } = "";
        
        public string _room { get; set; } = "";
        public string _days { get; set; } = "";
        
        public string _startTime { get; set; } = "";
        public string _endTime { get; set; } = "";
    }
}

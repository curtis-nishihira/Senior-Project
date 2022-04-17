using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.Models
{
    public class CapacityModel
    {
        public string _WeekdayName { get; set; } = "";
        public string _TimeSlot { get; set; } = "";
        public string _Building { get; set; } = "";

        public int _TotalValue { get; set; } = 0;

        public int _DefaultValue { get; set; } = 0; 

        public int _TotalSurveys { get; set; } = 0; 
        
    }
}

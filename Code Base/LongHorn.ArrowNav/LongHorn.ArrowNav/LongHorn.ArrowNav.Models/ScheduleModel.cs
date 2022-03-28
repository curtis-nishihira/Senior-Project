using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.Models
{
    public class ScheduleModel
    {
        public string _username { get; set; } = "";
        public List<StudentClassModel> _studentclasslist { get; set; } = new List<StudentClassModel>();
    }
}

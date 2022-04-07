using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.Models
{
    public class BuildingModel
    {
        public string acronym { get; set; } = "";
        public string buildingName { get; set; } = "";
        public double longitude { get; set; } = float.MinValue;
        public double latitude { get; set; } = float.MaxValue;

    }
}

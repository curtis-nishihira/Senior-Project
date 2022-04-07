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
        public float longitude { get; set; } = float.MinValue;
        public float latitude { get; set; } = float.MaxValue;

    }
}

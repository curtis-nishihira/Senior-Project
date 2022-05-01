using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.DAL
{ 
    public interface IBuildingRepository
    {
        public BuildingModel? RetrieveBuildingInfo(string name);
        public string? BuildingByAcronym(string acronym);

        public string? AcryonmByBuilding(string name);

        public List<string>? RetrieveAllBuildings();
    }
}

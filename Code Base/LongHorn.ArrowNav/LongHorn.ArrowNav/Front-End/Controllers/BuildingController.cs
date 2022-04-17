using Microsoft.AspNetCore.Mvc;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Managers;
using System.Text.Json;
using LongHorn.ArrowNav.DAL;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuildingController : ControllerBase
    {
        [HttpGet]
        [Route("getBuildingbyAcronym")]
        public string GetBuildingByAcronym(string acronym)
        {
            BuildingRepository repo = new BuildingRepository();
            var buildings = repo.BuildingByAcryonm(acronym);
            return buildings;
        }
        [HttpGet]
        [Route("getAllBuildings")]
        public List<string> GetAllBuidlings()
        {
            BuildingRepository repo = new BuildingRepository();
            List<string> buildings = repo.ReadAllBuildings();
            return buildings;
        }
        [HttpGet]
        [Route("getAcronymbyBuildingName")]
        public string GetAcronymbyBuilding(string BuildingName)
        {
            BuildingRepository repo = new BuildingRepository();
            string acronym = repo.AcryonmByBuilding(BuildingName);
            return acronym;
        }

        [HttpPost]
        [Route("getLatLong")]
        public BuildingModel GetLatLong(string BuildingName)
        {
            BuildingRepository repo = new BuildingRepository();
            BuildingModel model = repo.Read(BuildingName);
            return model;
        }

    }
}
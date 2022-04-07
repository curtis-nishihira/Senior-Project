using Microsoft.AspNetCore.Mvc;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Managers;
using System.Text.Json;
using LongHorn.ArrowNav.DAL.Implementation;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuildingController : ControllerBase
    {
        [HttpGet]
        [Route("getAllBuildings")]
        public List<string> GetAllBuidlings()
        {
            BuildingRepository repo = new BuildingRepository();
            List<string> buildings = repo.ReadAllBuildings();
            return buildings;
        }

        [HttpPost]
        [Route("getLatLong")]
        public BuildingModel GetLatLong(String BuildingName)
        {
            BuildingRepository repo = new BuildingRepository();
            BuildingModel model = repo.Read(BuildingName);
            return model;
        }

    }
}
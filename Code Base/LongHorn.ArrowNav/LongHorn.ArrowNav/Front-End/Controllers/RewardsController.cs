using Microsoft.AspNetCore.Mvc;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Managers;
using LongHorn.ArrowNav.DAL;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RewardsController : Controller
    {
        
        [HttpPost]
        [Route("InsertReward")]
        public string CreateReward(Rewards reward)
        {
            RewardsManager scheduleManager = new RewardsManager();
            var result = scheduleManager.Create(reward);
            return result;
        }
        [HttpPost]
        [Route("scheduledelete")]
        public string DeleteClass(Rewards reward)
        {
            RewardsManager scheduleManager = new RewardsManager();
            var result = scheduleManager.Delete(reward);
            return result;
        }
        [HttpPost]
        [Route("scheduleedit")]
        public string EditClass(Rewards reward)
        {
            RewardsManager scheduleManager = new RewardsManager();
            var result = scheduleManager.Edit(reward);
            return result;
        }
    }
        
 }
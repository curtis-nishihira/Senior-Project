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
        public string CreateRewardClass(Rewards reward)
        {
            RewardsManager scheduleManager = new RewardsManager();
            var result = scheduleManager.Create(reward);
            return result;
        }
        [HttpPost]
        [Route("DeleteReward")]
        public string DeleteRewardClass(Rewards reward)
        {
            RewardsManager scheduleManager = new RewardsManager();
            var result = scheduleManager.Delete(reward);
            return result;
        }
        [HttpPost]
        [Route("UpdateRewards")]
        public string EditRewardClass(Rewards reward)
        {
            RewardsManager scheduleManager = new RewardsManager();
            var result = scheduleManager.Edit(reward);
            return result;
        }
    }
        
 }
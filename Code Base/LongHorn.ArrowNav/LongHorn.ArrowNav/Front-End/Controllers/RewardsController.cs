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
            RewardsManager rewardManager = new RewardsManager();
            var result = rewardManager.Create(reward);
            return result;
        }
        [HttpPost]
        [Route("DeleteReward")]
        public string DeleteRewardClass(Rewards reward)
        {
            RewardsManager rewardManager = new RewardsManager();
            var result = rewardManager.Delete(reward);
            return result;
        }
        [HttpPost]
        [Route("UpdateRewards")]
        public string EditRewardClass(Rewards reward)
        {
            RewardsManager rewardManager = new RewardsManager();
            var result = rewardManager.Edit(reward);
            return result;
        }

        [HttpGet]
        [Route("GetCredits")]
        public int GetCreditClass(string email)
        {
            RewardsRepository repo= new RewardsRepository();
            var result = repo.GetCredits(email);
            return result;
        }

        [HttpGet]
        [Route("GetCounter")]
        public int GetCounterClass(string email)
        {
            RewardsRepository repo = new RewardsRepository();
            var result = repo.GetCounter(email);
            return result;
        }

        [HttpPost]
        [Route("SetCredits")]
        public string SetCreditClass(Rewards email)
        {
            RewardsRepository repo = new RewardsRepository();
            var result = repo.SetCredits(email);
            return result;
        }

        [HttpPost]
        [Route("SetCounter")]
        public string SetCounterClass(Rewards email)
        {
            RewardsRepository repo = new RewardsRepository();
            var result = repo.SetCounter(email);
            return result;
        }

    }
        
 }
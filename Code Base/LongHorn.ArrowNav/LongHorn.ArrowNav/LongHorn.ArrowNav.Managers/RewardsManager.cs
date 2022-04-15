using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.Managers
{
    public class RewardsManager
    {
        public RewardsManager()
        {

        }
        
        public string Create(Rewards reward)
        {
            CreateService createService = new CreateService();
            var result = createService.CreateRewardClass(reward);
            Log entry = new Log(result, "Info", "Data Layer", reward._Email);
            LogManager logManager = new LogManager();
            logManager.Log(entry);
            return result;
        }
        public string Delete(Rewards reward)
        {
            DeleteService deleteService = new DeleteService();
            var result = deleteService.DeleteRewardClass(reward);
            Log entry = new Log(result, "Info", "Data Layer", reward._Email);
            LogManager logManager = new LogManager();
            logManager.Log(entry);
            return result;
        }
        public string Edit(Rewards reward)
        {
            UpdateService updateService = new UpdateService();
            var result = updateService.EditRewardClass(reward);
            Log entry = new Log(result, "Info", "Data Layer", reward._Email);
            LogManager logManager = new LogManager();
            logManager.Log(entry);
            return result;
        }
        
    }
}
using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using System;
using LongHorn.ArrowNav.DAL;

namespace LongHorn.ArrowNav.Services
{
    public class CreateService : ICreateService
    {
        public string CreateAccount(AccountInfo info)
        {
            IRepository<AccountInfo> loggingRepository = new UMRepository();
            var result = loggingRepository.Create(info);
            return result;
        }
        public string CreateStudentClass(StudentClassModel studentclass)
        {
            IRepository<StudentClassModel> scheduleRepository = new ScheduleRepository();
            var result = scheduleRepository.Create(studentclass);
            return result;
        }

        public string CreateRewardClass(Rewards reward)
        {
            IRepository<Rewards> rewardRepository = new RewardsRepository();
            var result = rewardRepository.Create(reward);
            return result;
        }

    }
}
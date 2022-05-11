using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using System;

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
        public string CreateWellnessReminder(StudentWellnessModel studentWellnessModel)
        {
            IRepository<StudentWellnessModel> wellnessRepository = new WellnessRepository();
            var result = wellnessRepository.Create(studentWellnessModel);
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
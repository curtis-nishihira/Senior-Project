using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;
using LongHorn.ArrowNav.DAL;

namespace LongHorn.ArrowNav.Services
{
    public class UpdateService : IUpdateService
    {
        public string UpdateAccount(AccountInfo info)
        {
            IRepository<AccountInfo> umRepository = new UMRepository();
            var result = umRepository.Update(info);
            return result;
        }
        public string EditStudentClass(StudentClassModel studentclass)
        {
            IRepository<StudentClassModel> schedulerepository = new ScheduleRepository();
            var result = schedulerepository.Update(studentclass);
            return result;
        }
        public string EditWellnessReminder(StudentWellnessModel studentWellnessModel)
        {
            IRepository<StudentWellnessModel> wellnessRepository = new WellnessRepository();
            var result = wellnessRepository.Update(studentWellnessModel);
            return result;
        }


        public string EditRewardClass(Rewards reward)
        {
            IRepository<Rewards> rewardsrepository = new RewardsRepository();
            var result = rewardsrepository.Update(reward);
            return result;
        }

    }
}
using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongHorn.ArrowNav.Services
{
    public class UpdateService : IUpdateService
    {
        public string UpdateAccount(User info)
        {
            UMRepository umRepository = new UMRepository();
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

        public List<User> getAllUsers()
        {
            UMRepository umRepository = new UMRepository();
            var result = umRepository.getAllUsers();
            return result;

        }



        public string UpdateAttempts(string email)
        {
            var today = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            UMRepository umRepository = new UMRepository();
            var result = umRepository.UpdateFailedAttempts(email,today);
            return result;

        }
        public string UpdateSuccessfulAttempt(string email)
        {
            UMRepository umRepository = new UMRepository();
            var result = umRepository.UpdateSucessfulAttempt(email);
            return result;

        }

        public string UpdateAccount(AccountInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
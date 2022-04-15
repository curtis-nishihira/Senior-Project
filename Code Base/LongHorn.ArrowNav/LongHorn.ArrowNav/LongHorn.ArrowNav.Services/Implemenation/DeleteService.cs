using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Text;
using LongHorn.ArrowNav.DAL;

namespace LongHorn.ArrowNav.Services
{
    public class DeleteService : IDeleteService
    {
        public string DeleteAccount(string email)
        {
            UMRepository umRepository = new UMRepository();
            var result = umRepository.Delete(email);
            return result;
        }

        public string DeleteAccount(AccountInfo info)
        {
            throw new NotImplementedException();
        }

        public string DeleteStudentClass(StudentClassModel studentclass)
        {
            IRepository<StudentClassModel> schedulerepository = new ScheduleRepository();
            var result = schedulerepository.Delete(studentclass);
            return result;
        }
    }
}
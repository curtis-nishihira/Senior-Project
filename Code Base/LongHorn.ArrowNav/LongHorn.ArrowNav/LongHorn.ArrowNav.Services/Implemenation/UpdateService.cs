﻿using LongHorn.ArrowNav.DAL;
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
    }
}
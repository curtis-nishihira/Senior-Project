using LongHorn.DAL;
using System;
using System.Data.SqlClient;

namespace LongHorn.ArrowNav.Logging
{
    public class DatabaseLogService : ILogService
    {
        public bool Log(string description)
        {
            IRepository<string> r = new SqlDAO();
            var result = r.Create(description);
            return result;
        
        }
    }

}
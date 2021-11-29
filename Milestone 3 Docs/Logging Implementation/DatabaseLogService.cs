using System;
using System.Data.SqlClient;

namespace LongHorn.ArrowNav.Logging
{
    public class DatabaseLogService : ILogService
    {
        public bool Log(string description)
        {
            try
            {
                var connection = @"Server=localhost\SQLEXPRESS01;Database=Logging;Trusted_Connection=True";

                using (var conn = new SqlConnection(connection))
                {
                    
                    var sql = "Insert Into Logging Values ('"+ DateTime.Now.ToString() +" " + description + "');";
                    using (var command = new SqlCommand(sql, conn))
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                    return true;
            }
            catch 
            {
                return false;
            }
        }
    }

}
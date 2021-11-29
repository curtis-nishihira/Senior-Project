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
                var connection = @"Server=localhost\SQLEXPRESS01;Database=Logging;Trusted_Connection=True;";
                using (var conn = new SqlConnection(connection))
                {
                    var sql = "Insert Into Logging Values ('"+ DateTime.UtcNow + " " + description + "');";
                    using (var command = new SqlCommand(sql, conn))
                    {
                        command.ExecuteReader();
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
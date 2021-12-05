using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LongHorn.ArrowNav.DAL
{
    public class LoggingRepository : IRepository<string>
    {
        public bool Create(string description)
        {
            
            try
            {
                var connection = getConnection();


                using (var conn = new SqlConnection(connection))
                {
                    var time = DateTime.UtcNow.ToString();
                    //need to add the rest. Only does logs and timestamp for now
                    var sql = string.Format("INSERT INTO Logging (logs, UtcTimeStamp) " +
                        "VALUES('{0}', GETUTCDATE() );", description);
                    using (var command = new SqlCommand(sql, conn))
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                    }
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool Delete(string description)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBasedOnRangeOf(DateTime now, DateTime end)
        {
            try
            {
                var connection = getConnection();
                using (var conn = new SqlConnection(connection))
                {
                    var time = DateTime.UtcNow.ToString();
                    //need to add the rest. Only does logs and timestamp for now
                    var sql = string.Format("delete from Logging where UtcTimeStamp BETWEEN '{0}' AND '{1}';", now, end);
                    using (var command = new SqlCommand(sql, conn))
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                    }
                }
                return true; 
            }
            catch 
            {
                return false;
            }
        }

        public List<string> Read(string model)
        {
            throw new NotImplementedException();
        }

        public List<string> ReadAllBasedOnRangeOf(DateTime now, DateTime end)
        {
            List<string> retrievedLogs = new List<string>();
            var connection = getConnection();
            using (var conn = new SqlConnection(connection))
            {

                var time = DateTime.UtcNow.ToString();
                //going to get entries that are older than 30 days
                var sql = string.Format("select * from Logging where UtcTimeStamp BETWEEN '{0}' AND '{1}';", now, end);
                using (var command = new SqlCommand(sql, conn))
                {
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var entry = string.Format("{0} {1}", reader["logs"], reader["UtcTimeStamp"]);
                            retrievedLogs.Add(entry);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();
                    command.Connection.Close();
                }   
            }

            return retrievedLogs;
        }

        public bool Update(string description)
        {
            throw new NotImplementedException();
        }

        public string getConnection()
        {
            var connection = @"Server=localhost\SQLEXPRESS;Database=Logging;Trusted_Connection=True";
            return connection;
        }

    }
}

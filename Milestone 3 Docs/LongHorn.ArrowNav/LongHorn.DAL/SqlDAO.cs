using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LongHorn.ArrowNav.DAL
{
    public class SqlDAO : IRepository<string>
    {
        public bool Create(string description)
        {
            
            try
            {
                var connection = @"Server=localhost\SQLEXPRESS01;Database=Logging;Trusted_Connection=True";


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

        public List<string> Read(string model)
        {
            throw new NotImplementedException();
        }

        public bool Update(string description)
        {
            throw new NotImplementedException();
        }

    }
}

using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.DAL.Implementation
{
    public class RewardsRepository : IRepository<Rewards>
    {
        public string Create(Rewards model)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec InsertRewards '{0}', '{1}','{2}'", model._Email, model._Credits, model._Counter);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                return "Rewards Activated";
            }

            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string Delete(Rewards model)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec DeleteRewards '{0}', '{1}','{2}'", model._Email, model._Credits, model._Counter);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                return "Rewards Deleted";
            }

            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        public List<string> Read(Rewards model)
        {
            throw new NotImplementedException();
        }

        public string Update(Rewards model)
        {
            throw new NotImplementedException();
        }

        public string getConnection()
        {
            //var SQLConnectionString = ConfigurationManager.AppSettings.Get("LogsqlConnectionString");
            var SQLConnectionString = @"Server=tcp:arrownav-db.database.windows.net,1433;Initial Catalog=ArrowNavDB;Persist Security Info=False;User ID=brayan_admin;Password=Bf040800;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return SQLConnectionString;
        }

    }
}

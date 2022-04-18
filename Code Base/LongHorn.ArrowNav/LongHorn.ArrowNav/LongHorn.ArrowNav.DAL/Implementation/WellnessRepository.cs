using LongHorn.ArrowNav.Models;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.DAL.Implementation
{
    public class WellnessRepository : IRepository<StudentWellnessModel>
    {
        public string Create(StudentWellnessModel studentWellness)
        {
            try
            {
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec insertReminder '{0}', {1}, '{2}','{3}', {4}", studentWellness._Username, studentWellness._bodyWeight, studentWellness._startTime, studentWellness._endTime,studentWellness._waterIntake);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                        connection.Close();
                        return "Information saved";
                    }
                }
            }
            catch (Exception e)
            {
                return "error";
            }
        }

        public string Delete(StudentWellnessModel studentWellness)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec deleteReminder '{0}', '{1}', '{2}','{3}'", studentWellness._Username, studentWellness._bodyWeight, studentWellness._startTime, studentWellness._endTime);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                        connection.Close();
                        return "Delete Successful";
                    }
                }
            }


            catch (Exception e)
            {
                return "error";
            }
        }
        public int GetBodyWeight(string Username)
        {
            try
            {
                var bodyWeight = 0;
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec getBodyWeight '{0}'", Username);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            bodyWeight = ((int)reader["bodyWeight"]);
                        }
                        reader.Close();
                    }
                }
                return bodyWeight;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public string SetBodyWeight(StudentWellnessModel Username)
        {
            try
            {
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec setBodyWeight '{0}',{1}", Username._Username, Username._bodyWeight);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteReader();
                    }
                }
                return "Body Weight updated";
            }
            catch (Exception e)
            {
                return "error";
            }
        }
        public string GetStartTime(string Username)
        {
            try
            {
                var startTime = "";
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec getStartTime '{0}'", Username);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            startTime = ((string)reader["startTime"]);
                        }
                        reader.Close();
                    }
                }
                return startTime;
            }
            catch (Exception e)
            {
                return "error";
            }
        }
        public string SetStartTime(StudentWellnessModel Username)
        {
            try
            {
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec setStartTime '{0}','{1}'", Username._Username, Username._startTime);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteReader();
                    }
                }
                return "Start Time updated";
            }
            catch (Exception e)
            {
                return "error";
            }
        }
        public string GetEndTime(string Username)
        {
            try
            {
                var endTime = "";
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec getEndTime '{0}'", Username);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            endTime = ((string)reader["endTime"]);
                        }
                        reader.Close();
                    }
                }
                return endTime;
            }
            catch (Exception e)
            {
                return "error";
            }
        }
        public string SetEndTime(StudentWellnessModel Username)
        {
            try
            {
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec setStartTime '{0}','{1}'", Username._Username, Username._endTime);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteReader();
                    }
                }
                return "End Time updated";
            }
            catch (Exception e)
            {
                return "error";
            }
        }

        public List<string> Read(StudentWellnessModel model)
        {
            throw new NotImplementedException();
        }

        public string Update(StudentWellnessModel model)
        {
            try
            {
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec updateReminder '{0}', '{1}', '{2}','{3}'", model._Username, model._bodyWeight, model._startTime, model._endTime);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                        connection.Close();
                        return "Information updated";
                    }
                }
            }
            catch (Exception e)
            {
                return "error";
            }
        }
        public string getConnection()
        {
            //var SQLConnectionString = ConfigurationManager.AppSettings.Get("LogsqlConnectionString");
            return @"Server=localhost;Database=WellnessDatabase;Trusted_Connection=True";
            //var SQLConnectionString = @"Server=tcp:arrownav-db.database.windows.net,1433;Initial Catalog=ArrowNavDB;Persist Security Info=False;User ID=brayan_admin;Password=Bf040800;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //return SQLConnectionString;
        }
    }
}

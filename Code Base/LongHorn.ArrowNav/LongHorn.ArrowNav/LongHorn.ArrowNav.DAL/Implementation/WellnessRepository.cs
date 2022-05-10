using LongHorn.ArrowNav.Models;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LongHorn.ArrowNav.DAL
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
                    SqlCommand doesReminderExist = new SqlCommand("GetHydrationReminder", connection);

                    // Lets the SqlCommand Object know that its a store procedure type
                    doesReminderExist.CommandType = System.Data.CommandType.StoredProcedure;

                    // Adding the necessay parameters for the stored procedure
                    doesReminderExist.Parameters.Add(new SqlParameter("@email", studentWellness._Username));

                    using (SqlDataReader reader = doesReminderExist.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            reader.Close();
                            SqlCommand updateReminder = new SqlCommand("UpdateReminder", connection);

                            // Lets the SqlCommand Object know that its a store procedure type
                            updateReminder.CommandType = System.Data.CommandType.StoredProcedure;

                            // Adding the necessay parameters for the stored procedure
                            updateReminder.Parameters.Add(new SqlParameter("@email", studentWellness._Username));
                            updateReminder.Parameters.Add(new SqlParameter("@bodyWeight", studentWellness._bodyWeight));
                            updateReminder.Parameters.Add(new SqlParameter("@startTime", studentWellness._startTime));
                            updateReminder.Parameters.Add(new SqlParameter("@endTime", studentWellness._endTime));
                            updateReminder.Parameters.Add(new SqlParameter("@waterIntake", studentWellness._waterIntake));

                            updateReminder.ExecuteNonQuery();

                            return "Reminder Updated";
                        }
                        else
                        {
                            reader.Close();
                            var sqlStatement = string.Format("exec insertReminder '{0}', {1}, '{2}','{3}', {4}", studentWellness._Username, studentWellness._bodyWeight, studentWellness._startTime, studentWellness._endTime, studentWellness._waterIntake);
                            using (var command = new SqlCommand(sqlStatement, connection))
                            {
                                command.ExecuteNonQuery();
                                connection.Close();
                                return "Information saved";
                            }
                        }
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

        public bool GetReminder(string Username)
        {
            try
            {
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    SqlCommand getReminder = new SqlCommand("GetHydrationReminder", connection);

                    // Lets the SqlCommand Object know that its a store procedure type
                    getReminder.CommandType = System.Data.CommandType.StoredProcedure;

                    // Adding the necessay parameters for the stored procedure
                    getReminder.Parameters.Add(new SqlParameter("@email", Username));

                    using (SqlDataReader reader = getReminder.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return true;

                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return false;
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

        public double GetWaterIntake(string Username)
        {
            try
            {
                double waterIntake = 0.0;
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec getWaterIntake '{0}'", Username);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            waterIntake = (double)reader["waterIntake"];
                        }
                        reader.Close();
                    }
                }
                return waterIntake;
            }
            catch (Exception e)
            {
                return 0.0;
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
            var AzureConnectionString = ConfigurationManager.AppSettings.Get("DatabaseString");
            return AzureConnectionString;
        }
    }
}

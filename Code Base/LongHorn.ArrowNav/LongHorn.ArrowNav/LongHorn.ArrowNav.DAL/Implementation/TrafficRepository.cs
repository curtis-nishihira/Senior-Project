using LongHorn.ArrowNav.Models;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;


namespace LongHorn.ArrowNav.DAL
{
    public class TrafficRepository : IRepository<TrafficModel>
    {
        public string Create(TrafficModel model)
        {
            throw new NotImplementedException();
        }

        public string Delete(TrafficModel model)
        {
            throw new NotImplementedException();
        }

        public List<string> Read(TrafficModel model)
        {
            try
            {
                var connectionString = getConnection();
                string value = "";
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var checkTotalValues = string.Format("exec GetTotalValue '{0}', '{1}','{2}'", model._WeekdayName, model._ZoneName, model._TimeSlot);
                    using (var checkValue = new SqlCommand(checkTotalValues, connection))
                    {
                        SqlDataReader rdr = checkValue.ExecuteReader();
                        while (rdr.Read())
                        {
                            value = (string)rdr["TotalValue"];
                        }
                        rdr.Close();
                    }
                    connection.Close();
                }
                return new List<string> { value }; ;
            }
            catch (Exception ex)
            {
                return new List<string> { ex.ToString() };
            }
        }

        public List<string> GetAllValues(TrafficModel model)
        {
            List<string> retrievedValues = new List<string>();
            var sqlConnectionString = getConnection();
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                var sqlStatement = string.Format("exec GetTrafficWithTimeAndWeekDay '{0}','{1}'", model._TimeSlot, model._WeekdayName);
                using (var command = new SqlCommand(sqlStatement, connection))
                {
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var entry = string.Format("{0} {1} {2}", reader["ZoneName"], reader["TotalValue"],reader["TotalSurveys"]);
                            retrievedValues.Add(entry);
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

            return retrievedValues;
        }

        public string Update(TrafficModel model)
        {
            try
            {
                var addToTotalValues = 0;
                var connectionString = getConnection();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var checkTotalValues = string.Format("exec GetTotalValue '{0}', '{1}','{2}'", model._WeekdayName, model._ZoneName, model._TimeSlot);
                    using (var checkValue = new SqlCommand(checkTotalValues, connection))
                    {
                        int totalValues = 0;
                        SqlDataReader rdr = checkValue.ExecuteReader();
                        while(rdr.Read())
                        {
                            int temp = (int)rdr["TotalValue"];
                            totalValues = totalValues + temp;
                        }
                        addToTotalValues = totalValues + model._TotalValue;
                        rdr.Close();
                    }
                    var updatingValuesString = string.Format("exec UpdateTrafficValue {0},'{1}','{2}','{3}'", addToTotalValues, model._WeekdayName, model._ZoneName, model._TimeSlot);
                    using (var updateValues = new SqlCommand(updatingValuesString, connection))
                    {
                        updateValues.ExecuteNonQuery();
                    }
                    var addTotalSurveys = 0;
                    var checkTotalSurveys = string.Format("exec GetTotalSurveys '{0}', '{1}','{2}'", model._WeekdayName, model._ZoneName, model._TimeSlot);
                    using (var checkSurveys = new SqlCommand(checkTotalSurveys, connection))
                    {
                        int totalSurveys = 0;
                        SqlDataReader rdr = checkSurveys.ExecuteReader();
                        while (rdr.Read())
                        {
                            int temp = (int)rdr["TotalSurveys"];
                            totalSurveys = totalSurveys + temp;
                        }
                        if (model._TotalValue > 0)
                        {
                            addTotalSurveys = totalSurveys + 1;
                        }
                        rdr.Close();
                    }
                    var updatingSurveysString = string.Format("exec UpdateTrafficSurveys {0},'{1}','{2}','{3}'", addTotalSurveys, model._WeekdayName, model._ZoneName, model._TimeSlot);
                    using (var updateSurveys = new SqlCommand(updatingValuesString, connection))
                    {
                        updateSurveys.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return "values updated " + " " +addToTotalValues;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }


        }
        public string getConnection()
        {
            //var SQLConnectionString = ConfigurationManager.AppSettings.Get("UMsqlConnectionString");
            //return @"Server=localhost\SQLEXPRESS01;Database=ArrowNav;Trusted_Connection=True";
            var AzureConnectionString = @"Server=tcp:arrownav-db.database.windows.net,1433;Initial Catalog=ArrowNavDB;Persist Security Info=False;User ID=brayan_admin;Password=Bf040800;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return AzureConnectionString;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongHorn.ArrowNav.Models;

namespace LongHorn.ArrowNav.DAL
{
    public class CapacityRepository : IRepository<CapacitySurveyModel>
    {
        LoggingRepository logRepository = new LoggingRepository();

        // methods from interface but do not have need yet
        public string Create(CapacitySurveyModel model)
        {
            throw new NotImplementedException();
        }

        public string Delete(CapacitySurveyModel model)
        {
            throw new NotImplementedException();
        }

        public List<string> Read(CapacitySurveyModel model)
        {
            throw new NotImplementedException();
        }
        public string GetBuildingHours(CapacitySurveyModel model)
        {
            Log entry;
            try
            {
                string returnValue = "";
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    CapacityModel returnModel = new CapacityModel();
                    // two parameters are building name and the weekday must be in this order
                    var sqlStatement = string.Format("exec GetBuildingHours '{0}','{1}'", model._Building, model._WeekdayName);

                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.Connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // output should be return as HH:MM-HH:MM format
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                returnValue += reader["START"].ToString() + "-";
                                returnValue += reader["END"].ToString();
                            }
                        }
                        else
                        {
                            returnValue += "ERROR";
                        }
                        reader.Close();
                        command.Connection.Close();
                    }
                    entry = new Log("getBuildingHours Sucessfully Called", "DAL", "Info", "User");
                    logRepository.Create(entry);
                    return returnValue;
                }
            }
            catch (Exception ex)
            {
                entry = new Log("GetSingleCapacity Unsucessfully Called", "DAL", "Error", "User");
                logRepository.Create(entry);
                return "Error: Capacity Repository Failure -> getBuildingHours()";
            }
        }

        public CapacityModel GetSingleCapacity(CapacitySurveyModel model)
        {
            Log entry;
            try
            {
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    CapacityModel returnModel = new CapacityModel();

                    // stored procedure takes three parameters time, weekday, and building must be in this order
                    var sqlStatement = string.Format("exec GetCapacity '{0}','{1}','{2}'", model._TimeSlot, model._WeekdayName, model._Building);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.Connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                returnModel._WeekdayName = (string)reader["WeekdayName"];
                                returnModel._Building = (string)reader["Building"];
                                returnModel._TotalValue = Int16.Parse((string)reader["TotalValue"]);
                                returnModel._TimeSlot = reader["TimeSlot"].ToString();
                                returnModel._TotalSurveys = Int16.Parse((string)reader["TotalSurveys"]);
                                returnModel._DefaultValue = Int16.Parse((string)reader["DefaultValue"]);
                            }
                        }
                        else
                        {
                            returnModel = new CapacityModel();
                        }
                        reader.Close();
                        command.Connection.Close();
                    }
                    entry = new Log("getBuildingHours Sucessfully Called", "DAL", "Info", "User");
                    logRepository.Create(entry);
                    return returnModel;
                }
            }
            catch(Exception ex)
            {
                CapacityModel returnModel = new CapacityModel();
                returnModel._WeekdayName = "Error: Capacity Repository Failure -> getSingleCapacity()";
                entry = new Log("GetSingleCapacity Unsucessfully Called", "DAL", "Error", "User");
                logRepository.Create(entry);
                return returnModel;
            } 
        }

        public string Update(CapacitySurveyModel model)
        {
            Log entry;
            try
            {
                var connectionString = getConnection();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // updates the total capacity value of the input building and time period
                    var updatingValuesString = string.Format("exec UpdateCapacity {0},'{1}','{2}','{3}'", model._AddValue, model._WeekdayName, model._Building, model._TimeSlot);
                    using (var updateValues = new SqlCommand(updatingValuesString, connection))
                    {
                        updateValues.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                entry = new Log("Update Sucessfully Called", "DAL", "Info", "User");
                logRepository.Create(entry);
                return "surveys updated";
            }
            catch (Exception ex)
            {
                entry = new Log("Update Unsucessfully Called", "DAL", "Error", "User");
                logRepository.Create(entry);
                return "Error: Capacity Repository Failure -> Update()";
            }
        }

       
        public string getConnection()
        {

            var AzureConnectionString = ConfigurationManager.AppSettings.Get("DatabaseString");
            return AzureConnectionString;
        }
    }
}

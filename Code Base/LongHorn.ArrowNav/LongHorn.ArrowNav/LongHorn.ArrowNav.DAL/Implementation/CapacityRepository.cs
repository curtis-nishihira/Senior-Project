using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongHorn.ArrowNav.Models;

namespace LongHorn.ArrowNav.DAL
{
    public class CapacityRepository : IRepository<CapacitySurveyModel>
    {
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
        public string getBuildingHours(CapacitySurveyModel model)
        {
            string returnValue = "";
            var sqlConnectionString = getConnection();
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                CapacityModel returnModel = new CapacityModel();
                // sql injection? and also shouold speciufy what is what in paarameetrs if you leave
                var sqlStatement = string.Format("exec GetBuildingHours '{0}','{1}'", model._Building, model._WeekdayName);
                using (var command = new SqlCommand(sqlStatement, connection))
                {
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            returnValue += (string)reader["START"] + "-";
                            returnValue += (string)reader["END"];
                        }
                    }
                    else
                    {
                        // is empty
                        returnValue += "ERROR";
                    }
                    reader.Close();
                    command.Connection.Close();
                }
                return returnValue;
            }
        }


        public CapacityModel getSingleCapacity(CapacitySurveyModel model)
        {
            var sqlConnectionString = getConnection();
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                CapacityModel returnModel = new CapacityModel();
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
                        // is empty
                        returnModel = new CapacityModel(); 
                    }
                    reader.Close();
                    command.Connection.Close();
                }
                return returnModel;
            }
        }

        public string Update(CapacitySurveyModel model)
        {
            try
            {
                var connectionString = getConnection();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var updatingValuesString = string.Format("exec UpdateCapacity {0},'{1}','{2}','{3}'", model._AddValue, model._WeekdayName, model._Building, model._TimeSlot);
                    using (var updateValues = new SqlCommand(updatingValuesString, connection))
                    {
                        updateValues.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return "surveys updated";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

       
        public string getConnection()
        {
            // needs to be config or pass
            var AzureConnectionString = @"Server=tcp:arrownav-db.database.windows.net,1433;Initial Catalog=ArrowNavDB;Persist Security Info=False;User ID=brayan_admin;Password=Bf040800;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return AzureConnectionString;
        }
    }
}

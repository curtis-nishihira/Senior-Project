using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongHorn.ArrowNav.Models;

namespace LongHorn.ArrowNav.DAL
{
    public class BuildingRepository : IRepository<BuildingModel>
    {
        public string Create(BuildingModel model)
        {
            throw new NotImplementedException();
        }

        public string Delete(BuildingModel model)
        {
            throw new NotImplementedException();
        }

        public BuildingModel Read(String name)
        {
            try
            {
                var connectionString = getConnection();
                BuildingModel model = new BuildingModel();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec GetBuilding '{0}'", name);
                    using (var checkValue = new SqlCommand(sqlStatement, connection))
                    {
                        SqlDataReader rdr = checkValue.ExecuteReader();
                        while (rdr.Read())
                        {
                            model.acronym = (string)rdr["Acronym"];
                            model.latitude = (double)rdr["Latitude"];
                            model.longitude = (double)rdr["Longitude"];
                            model.buildingName = name;
                        }
                        rdr.Close();
                    }
                    connection.Close();
                }
                return model;
            }
            catch (Exception ex)
            {
                BuildingModel model = new BuildingModel();
                model.buildingName = ex.ToString();
                return model;
            }
        }
        public string BuildingByAcryonm(string acronym)
        {
            try
            {
                var sqlConnectionString = getConnection();
                
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    string temp = "";
                    var sqlStatement = string.Format("exec GetBuildingNameByAcronym '{0}'", acronym);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.Connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        
                        while (reader.Read())
                        {
                            temp = string.Format("{0}", reader["BuildingName"]);
                        }
                        reader.Close();
                    }
                    connection.Close();
                    return temp;
                }
               


            }
            catch(Exception e)
            {
                return "Error";
            }
        }

        public string AcryonmByBuilding(string buildingname)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    string temp = "";
                    var sqlStatement = string.Format("exec GetAcronymsbyBuildingNames '{0}'", buildingname);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.Connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            temp = string.Format("{0}", reader["Acronym"]);
                        }
                        reader.Close();
                    }
                    connection.Close();
                    return temp;
                }



            }
            catch (Exception e)
            {
                return "Error";
            }
        }
        public List<string> ReadAllBuildings()
        {
            List<string> retrievedValues = new List<string>();
            var sqlConnectionString = getConnection();
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                var sqlStatement = string.Format("exec GetAllBuildings");
                using (var command = new SqlCommand(sqlStatement, connection))
                {
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var entry = string.Format("{0}", reader["BuildingName"]);
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

        public List<string> ReadAllAcronyms()
        {
            List<string> retrievedValues = new List<string>();
            var sqlConnectionString = getConnection();
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                var sqlStatement = string.Format("exec GetBuildingAcronyms");
                using (var command = new SqlCommand(sqlStatement, connection))
                {
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var entry = string.Format("{0}", reader["Acronym"]);
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

        public string Update(BuildingModel model)
        {
            throw new NotImplementedException();
        }

        public string getConnection()
        {
            //return @"Server=localhost\SQLEXPRESS01;Database=ArrowNav;Trusted_Connection=True";
            var AzureConnectionString = @"Server=tcp:arrownav-db.database.windows.net,1433;Initial Catalog=ArrowNavDB;Persist Security Info=False;User ID=brayan_admin;Password=Bf040800;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return AzureConnectionString;

        }

        public List<string> Read(BuildingModel model)
        {
            throw new NotImplementedException();
        }
    }
}

using LongHorn.ArrowNav.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace LongHorn.ArrowNav.DAL
{
    public class BuildingRepository : IBuildingRepository
    {
        LoggingRepository logRepository = new LoggingRepository();

        Log? logEntry;

        // Returns all the information of the building based off of the building name.
        public BuildingModel? RetrieveBuildingInfo(string name)
        {
            try
            {
                var connectionString = GetConnection();
                BuildingModel model = new BuildingModel();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand getBuildingCmd = new SqlCommand("GetBuilding", connection);

                    // Lets the SqlCommand Object know that its a store procedure type
                    getBuildingCmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Adding the necessay parameters for the stored procedure
                    getBuildingCmd.Parameters.Add(new SqlParameter("@NAME", name));

                    using (SqlDataReader reader = getBuildingCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            model.acronym = (string)reader["Acronym"];

                            model.latitude = (double)reader["Latitude"];

                            model.longitude = (double)reader["Longitude"];

                            model.buildingName = name;

                        }

                    }

                    connection.Close();
                }
                try
                {
                    logEntry = new Log("RetieveBuildingInfo Successfully Called", "DAL", "Info", "User");

                    logRepository.Create(logEntry);
                }
                catch
                {

                    // Ignoring exceptions being thrown that might prevent business functionality from working

                }

                return model;
            }

            // Logging the possible execptions.
            catch (SqlException ex)
            {
                logEntry = new Log(ex.Message, "DAL: " + ex.StackTrace, "Error", "User");
                logRepository.Create(logEntry);
                return null;
            }
            catch (IndexOutOfRangeException ex)
            {
                logEntry = new Log(ex.Message, "DAL: " + ex.StackTrace, "Error", "User");
                logRepository.Create(logEntry);
                return null;
            }
            catch (NullReferenceException ex)
            {
                logEntry = new Log(ex.Message, "DAL: " + ex.StackTrace, "Error", "User");
                logRepository.Create(logEntry);
                return null;
            }

        }

        // Searches for a building using the acronym 
        public string? BuildingByAcronym(string acronym)
        {
            try
            {
                var sqlConnectionString = GetConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    string buildingName = "";

                    SqlCommand getBuildingByAcronymCmd = new SqlCommand("GetBuildingNameByAcronym", connection);

                    // Lets the SqlCommand Object know that its a store procedure type
                    getBuildingByAcronymCmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Adding the necessay parameters for the stored procedure
                    getBuildingByAcronymCmd.Parameters.Add(new SqlParameter("@ACRONYM", acronym));
                    using (SqlDataReader reader = getBuildingByAcronymCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            buildingName = string.Format("{0}", reader["BuildingName"]);

                        }
                        else
                        {
                            buildingName = "Building not Found in Database";
                        }

                    }
                    logEntry = new Log("Building By Acronym Successfully Called", "DAL", "Info", "User");

                    logRepository.Create(logEntry);

                    return buildingName;

                }

            }
            // Logging the possible execptions.
            catch (SqlException ex)
            {
                logEntry = new Log(ex.Message, "DAL: " + ex.StackTrace, "Error", "User");

                logRepository.Create(logEntry);

                return null;
            }
            catch (IndexOutOfRangeException ex)
            {
                logEntry = new Log(ex.Message, "DAL: " + ex.StackTrace, "Error", "User");

                logRepository.Create(logEntry);

                return null;
            }
            catch (NullReferenceException ex)
            {
                logEntry = new Log(ex.Message, "DAL: " + ex.StackTrace, "Error", "User");

                logRepository.Create(logEntry);

                return null;
            }

        }

        // Searches for the acronym of a building by using their respective building name
        public string? AcryonmByBuilding(string buildingname)
        {
            try
            {
                var sqlConnectionString = GetConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    string acronym = "";

                    SqlCommand getAcronymByBuildingCmd = new SqlCommand("GetAcronymsbyBuildingNames", connection);

                    //lets the SqlCommand Object know that its a store procedure type
                    getAcronymByBuildingCmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //adding the necessay parameters for the stored procedure
                    getAcronymByBuildingCmd.Parameters.Add(new SqlParameter("@BuildingName", buildingname));

                    using (SqlDataReader reader = getAcronymByBuildingCmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            reader.Read();
                            acronym = string.Format("{0}", reader["Acronym"]);

                        }

                    }
                    logEntry = new Log("Acronym By Building Successfully Called", "DAL", "Info", "User");

                    logRepository.Create(logEntry);

                    return acronym;
                }

            }

            //Logging the possible execptions.
            catch (SqlException ex)
            {
                logEntry = new Log(ex.Message, "DAL: " + ex.StackTrace, "Error", "User");

                logRepository.Create(logEntry);

                return null;
            }
            catch (IndexOutOfRangeException ex)
            {
                logEntry = new Log(ex.Message, "DAL: " + ex.StackTrace, "Error", "User");

                logRepository.Create(logEntry);

                return null;

            }
            catch (NullReferenceException ex)
            {
                logEntry = new Log(ex.Message, "DAL: " + ex.StackTrace, "Error", "User");

                logRepository.Create(logEntry);

                return null;

            }
        }

        // Returns all of the buildings we have in our database.
        public List<string>? RetrieveAllBuildings()
        {
            List<string> buildingsList = new List<string>();

            try
            {
                var sqlConnectionString = GetConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    SqlCommand getAllBuildingsCmd = new SqlCommand("GetAllBuildings", connection);

                    // Lets the SqlCommand Object know that its a store procedure type
                    getAllBuildingsCmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader reader = getAllBuildingsCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                var entry = string.Format("{0}", reader["BuildingName"]);
                                buildingsList.Add(entry);
                            }

                        }
                    }
                }
                logEntry = new Log("Read All Buildings Successfully Called", "DAL", "Info", "User");

                logRepository.Create(logEntry);

                return buildingsList;
            }
            catch (SqlException ex)
            {
                logEntry = new Log(ex.Message, "DAL: " + ex.StackTrace, "Error", "User");

                logRepository.Create(logEntry);

                return null;
            }
            catch (IndexOutOfRangeException ex)
            {
                logEntry = new Log(ex.Message, "DAL: " + ex.StackTrace, "Error", "User");

                logRepository.Create(logEntry);

                return null;

            }
            catch (NullReferenceException ex)
            {
                logEntry = new Log(ex.Message, "DAL: " + ex.StackTrace, "Error", "User");

                logRepository.Create(logEntry);

                return null;

            }


        }

        private string? GetConnection()
        {
            var AzureConnectionString = ConfigurationManager.AppSettings.Get("DatabaseString");
            return AzureConnectionString;

        }

    }
}

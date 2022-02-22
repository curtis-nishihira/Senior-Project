﻿using LongHorn.ArrowNav.Models;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;


namespace LongHorn.ArrowNav.DAL
{
    public class LoggingRepository : IRepository<Log>
    {
        public string Create(Log logEntry)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("INSERT INTO Logging (logs, UtcTimeStamp, logLevel, userPerformingOperator, category ) " +
                        "VALUES('{0}', '{1}','{2}','{3}','{4}');", logEntry._Log, logEntry._UtcTime, logEntry._Level, logEntry._User, logEntry._Type);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();

                    }
                    var savedSqlStatement = string.Format("select * from Logging where UtcTimeStamp = '{0}'", logEntry._UtcTime);
                    using (var checkSave = new SqlCommand(savedSqlStatement, connection))
                    {
                        SqlDataReader reader = checkSave.ExecuteReader();

                        if (reader.HasRows)
                        {
                            connection.Close();
                            return "Successful Log";
                        }
                        else
                        {
                            connection.Close();
                            return "Log was not saved onto the data store";
                        }
                    }

                }

            }
            catch (SqlException e)
            {
                return "Data Accces Layer error";
            }
        }

        public string Delete(Log entry)
        {
            throw new NotImplementedException();
        }

        public string DeleteBasedOnRangeOf(DateTime start, DateTime now)
        {
            try
            {
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    var deleteSqlStatement = string.Format("delete from Logging where UtcTimeStamp BETWEEN '{0}' AND '{1}';", start, now);
                    using (var command = new SqlCommand(deleteSqlStatement, connection))
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                    }

                }
                return "Successful Removal";
            }
            catch
            {
                return "Unable to remove the logs successsfully";
            }
        }

        public List<string> Read(Log logEntry)
        {
            throw new NotImplementedException();
        }

        public List<string> ReadAllBasedOnRangeOf(DateTime start, DateTime end)
        {

            List<string> retrievedLogs = new List<string>();
            var sqlConnectionString = getConnection();
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                //going to get entries that are older than 30 days
                var sqlStatement = string.Format("select * from Logging where UtcTimeStamp BETWEEN '{0}' AND '{1}';", start, end);
                using (var command = new SqlCommand(sqlStatement, connection))
                {
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var entry = string.Format("{0} {1} {2} {3} {4}", reader["logs"], reader["UtcTimeStamp"], reader["logLevel"], reader["userPerformingOperator"], reader["category"]);
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

        public string Update(Log logEntry)
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
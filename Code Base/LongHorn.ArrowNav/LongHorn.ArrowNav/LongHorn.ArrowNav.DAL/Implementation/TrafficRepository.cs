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
            throw new NotImplementedException();
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
                    var checkTotalValues = string.Format("Select TotalValue from traffic where WeekdayName = '{0}' and ZoneName = '{1}' and TimeSlot = '{2}'", model._WeekdayName, model._ZoneName, model._TimeSlot);
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
                    var updatingValuesString = string.Format("Update traffic set TotalValue = {0} where  WeekdayName = '{1}' and ZoneName = '{2}' and TimeSlot = '{3}'", addToTotalValues, model._WeekdayName, model._ZoneName, model._TimeSlot);
                    using (var updateValues = new SqlCommand(updatingValuesString, connection))
                    {
                        updateValues.ExecuteNonQuery();
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
            return @"Server=localhost\SQLEXPRESS;Database=ArrowNav;Trusted_Connection=True";
            //var AzureConnectionString = @"Server=tcp:arrownav-db.database.windows.net,1433;Initial Catalog=ArrowNavDB;Persist Security Info=False;User ID=brayan_admin;Password=Bf040800;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //return AzureConnectionString;
        }
    }
}

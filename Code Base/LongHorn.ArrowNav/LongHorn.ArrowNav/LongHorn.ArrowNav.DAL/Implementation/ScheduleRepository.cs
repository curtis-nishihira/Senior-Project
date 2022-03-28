using LongHorn.ArrowNav.Models;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.DAL
{
    public class ScheduleRepository : IRepository<ScheduleModel>
    {
        public string Create(ScheduleModel schedule)
        {
            try
            {
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    //List<StudentClassModel> studentClassModels = new List<StudentClassModel>();
                    var checkScheduleExistence = string.Format("exec GetScheduleByUser '{0}' ", schedule._username);
                    using (var checkSchedule = new SqlCommand(checkScheduleExistence, connection))
                    {
                        SqlDataReader rdr = checkSchedule.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            rdr.Close();
                            connection.Close();
                            return "Schedule already exists";
                        }
                        else
                        {
                            rdr.Close();
                            var addSchedule = string.Format("exec AddSchedule '{0}', '{1}'", schedule._username ,schedule._studentclasslist);
                            using (var addCommand = new SqlCommand(addSchedule, connection))
                            {
                                addCommand.ExecuteNonQuery();
                            }
                            var savedSqlStatement = string.Format("exec GetScheduleByUsername '{0}' ", schedule._username);
                            using (var checkSave = new SqlCommand(savedSqlStatement, connection))
                            {
                                SqlDataReader scheduleReader = checkSave.ExecuteReader();

                                if (scheduleReader.HasRows)
                                {
                                    scheduleReader.Close();
                                    connection.Close();
                                    return "Successful Schedule Creation";
                                }
                                else
                                {
                                    scheduleReader.Close();
                                    connection.Close();
                                    return "Schedule was not saved onto the data store";
                                }
                            }
                        }
                    }
                }

            }
            catch (SqlException e)
            {
                return e.ToString();
            }
        }

        public string Delete(ScheduleModel schedule)
        {
            throw new NotImplementedException();
        }

        public List<string> Read(ScheduleModel schedule)
        {
            throw new NotImplementedException();
        }

        public string Update(ScheduleModel schedule)
        {
            throw new NotImplementedException();
        }

        public string AddStudentClass (StudentClassModel studentclass)
        {
            throw new NotImplementedException();
        }
        public string DeleteStudentClass(StudentClassModel studentclass)
        {
            throw new NotImplementedException();
        }
        public string EditStudentClass(StudentClassModel studentclass)
        {
            throw new NotImplementedException();
        }
        public string getConnection()
        {
            //var SQLConnectionString = Get("UMsqlConnectionString");
            return @"Server=LAPTOP-KI9GTVUJ\SQLEXPRESS01;Database=ArrowNav;Trusted_Connection=True";
            //var AzureConnectionString = @"Server=tcp:arrownav-db.database.windows.net,1433;Initial Catalog=ArrowNavDB;Persist Security Info=False;User ID=brayan_admin;Password=Bf040800;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //return AzureConnectionString;
        }
    }
}

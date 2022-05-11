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
    public class ScheduleRepository : IRepository<StudentClassModel>
    {
        public string Create(StudentClassModel studentclass)
        {
            try
            {
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    //checks if the class already exists
                    var checkClassExistence = string.Format("exec GetClassByPK '{0}', '{1}', '{2}' ", studentclass._Username, studentclass._course, studentclass._coursetype);
                    using (var checkClass = new SqlCommand(checkClassExistence, connection))
                    {
                        SqlDataReader reader = checkClass.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Close();
                            connection.Close();
                            return "Student class already exists in the schedule";
                        }
                        else
                        {
                            reader.Close();
                            var addStudentClass = string.Format("exec AddStudentClass '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'", studentclass._Username, 
                                studentclass._course, studentclass._coursetype, studentclass._building, studentclass._room, studentclass._days, studentclass._startTime, studentclass._endTime);
                            using (var addCommand = new SqlCommand(addStudentClass, connection))
                            {
                                addCommand.ExecuteNonQuery();
                            }


                            var savedSqlStatement = string.Format("exec GetClassByPK '{0}', '{1}', '{2}' ", studentclass._Username, studentclass._course, studentclass._coursetype);
                            using (var checkSave = new SqlCommand(savedSqlStatement, connection))
                            {
                                SqlDataReader userReader = checkSave.ExecuteReader();

                                if (userReader.HasRows)
                                {
                                    userReader.Close();
                                    connection.Close();
                                    return "Class Added successfully to schedule";
                                }
                                else
                                {
                                    userReader.Close();
                                    connection.Close();
                                    return "Class was not saved onto the data store";
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

        public string Delete(StudentClassModel studentclass)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec deleteStudentClass '{0}', '{1}', '{2}' ", studentclass._Username, studentclass._course, studentclass._coursetype);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    var savedSqlStatement = string.Format("exec GetClassByPK '{0}', '{1}', '{2}' ", studentclass._Username, studentclass._course, studentclass._coursetype);
                    using (var checkSave = new SqlCommand(savedSqlStatement, connection))
                    {
                        SqlDataReader reader = checkSave.ExecuteReader();

                        if (reader.HasRows)
                        {
                            connection.Close();
                            return "Unsuccessful Class Deletion from schedule";
                        }
                        else
                        {
                            connection.Close();
                            return "Class was deleted successfully from the schedule";
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                return "Data Access Layer error.";
            }

        }

        public List<StudentClassModel> Read(String name)
        {
            try
            {
                var connectionString = getConnection();
                List<StudentClassModel> list = new List<StudentClassModel>();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    var sqlStatement = string.Format("exec GetScheduleByUsername '{0}'", name);
                    using (var checkValue = new SqlCommand(sqlStatement, connection))
                    {
                        SqlDataReader rdr = checkValue.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                StudentClassModel model = new StudentClassModel();
                                model._course = (string)rdr["course"];
                                model._coursetype = (string)rdr["coursetype"];
                                model._building = (string)rdr["building"];
                                model._room = (string)rdr["room"];
                                model._days = (string)rdr["days"];
                                model._startTime = (string)rdr["starttime"];
                                model._endTime = (string)rdr["endtime"];
                                list.Add(model);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows found.");
                        }
                        rdr.Close();
                    }
                    connection.Close();
                }
                return list;
            }
            catch (Exception ex)
            {
                StudentClassModel model = new StudentClassModel();
                model._Username = ex.ToString();
                List<StudentClassModel> list = new List<StudentClassModel>();
                list.Add(model);
                return list;
            }
        }

      
        public string Update(StudentClassModel studentclass)
        {
            try
            {
                var sqlConnectionString = getConnection();


                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    //checks if the class already exists
                    var checkClassExistence = string.Format("exec GetClassByPK '{0}', '{1}', '{2}' ", studentclass._Username, studentclass._course, studentclass._coursetype);
                    using (var checkClass = new SqlCommand(checkClassExistence, connection))
                    {
                        SqlDataReader reader = checkClass.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Close();
                            var editStudentClass = string.Format("exec EditStudentClass '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'", studentclass._Username, studentclass._course, studentclass._coursetype, studentclass._building, studentclass._room, studentclass._days, studentclass._startTime, studentclass._endTime);
                            using (var editClass = new SqlCommand(editStudentClass, connection))
                            {
                                editClass.ExecuteNonQuery();
                                return "Class has been edited";
                            }
                        }
                        else
                        {
                            reader.Close();
                            connection.Close();
                            return "Class does not exist. Class must be created before classes can be edited.";
                        }

                    }
                }

            }
            catch (SqlException e)
            {
                return e.ToString();
            }
        }

        public List<string> GetTodayClass(string letters, string email)
        {
            try
            {
                List<string> list = new List<string>();
                var sqlConnectionString = getConnection();


                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    //checks if the class already exists
                    var checkClassExistence = string.Format("exec TodayClass '{0}', '{1}'", letters,email);
                    using (var checkClass = new SqlCommand(checkClassExistence, connection))
                    {
                        SqlDataReader reader = checkClass.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var buildingAcronym = (string)reader["building"];
                                list.Add(buildingAcronym);
                            }
                            
                        }

                    }
                }
                return list;

            }
            catch (SqlException e)
            {
                List<string> list = new List<string>();
                return list;
            }
        }

        public string getConnection()
        {
            var AzureConnectionString = ConfigurationManager.AppSettings.Get("DatabaseString");
            return AzureConnectionString;
        }

        public List<string> Read(StudentClassModel model)
        {
            throw new NotImplementedException();
        }

    }
}
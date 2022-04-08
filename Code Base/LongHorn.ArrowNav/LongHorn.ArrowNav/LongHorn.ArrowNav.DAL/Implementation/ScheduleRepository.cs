using LongHorn.ArrowNav.Models;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            var addStudentClass = string.Format("exec AddStudentClass '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'", studentclass._Username, studentclass._course, studentclass._coursetype, studentclass._building, studentclass._room, studentclass._days, studentclass._startTime, studentclass._endTime);
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

        public List<string> Read(StudentClassModel studentclass)
        {
            throw new NotImplementedException();
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

        public string getConnection()
        {
            //var SQLConnectionString = Get("UMsqlConnectionString");
            return @"Server=LAPTOP-KI9GTVUJ\SQLEXPRESS01;Database=ArrowNav;Trusted_Connection=True";
            //var AzureConnectionString = @"Server=tcp:arrownav-db.database.windows.net,1433;Initial Catalog=ArrowNavDB;Persist Security Info=False;User ID=brayan_admin;Password=Bf040800;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //return AzureConnectionString;
        }
    }
}
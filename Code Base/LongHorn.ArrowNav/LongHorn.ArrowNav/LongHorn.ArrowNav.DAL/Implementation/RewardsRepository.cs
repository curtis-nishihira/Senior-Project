using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.DAL
{
    public class RewardsRepository : IRepository<Rewards>
    {
        public string Create(Rewards model)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec InsertRewards '{0}'", model._Email);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                return "Rewards Activated";
            }

            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string Delete(Rewards model)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec DeleteRewards '{0}'", model._Email);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                return "Rewards Deleted";
            }

            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        public List<string> Read(Rewards model)
        {
            throw new NotImplementedException();
        }

        public string Update(Rewards model)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec UpdateRewards '{0}', '{1}','{2}'", model._Email, model._Credits, model._Counter);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                return "Rewards Updated";
            }

            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public int GetCredits(string email)
        {
            try
            {
                var credits = 0;
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec GetCredits '{0}'", email);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            credits = ((int)reader["credits"]);
                        }
                        reader.Close();
                    }
                }

                return credits;
            }

            catch (Exception ex)
            {
                return 0;
            }
        }

        public int GetCounter(string email)
        {
            try
            {
                var counter = 0;
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec GetCounter '{0}'", email);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            counter = ((int)reader["counter"]);
                        }
                        reader.Close();
                    }
                }

                return counter;
            }

            catch (Exception ex)
            {
                return 0;
            }
        }

        public string SetCredits(Rewards email)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec SetCredit '{0}', {1}", email._Email, email._Credits);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteReader();
                    }
                }

                return "Credits updated";
            }

            catch (Exception ex)
            {
                return "error";
            }
        }


        public string SetCounter(Rewards email)
        {
            try
            {
                var counter = 0;
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec SetCounter '{0}', {1}", email._Email, email._Counter);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteReader();
                    }
                }

                return "counter updated";
            }

            catch (Exception ex)
            {
                return "error";
            }
        }


        public string getConnection()
        {
            return @"Server=localhost\SQLEXPRESS;Database=ArrowNav;Trusted_Connection=True";
            
        }

    }
}
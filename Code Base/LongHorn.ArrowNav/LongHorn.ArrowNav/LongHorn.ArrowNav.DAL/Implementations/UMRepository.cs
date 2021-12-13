using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LongHorn.ArrowNav.DAL
{
    public class UMRepository : IRepository<AccountInfo>
    {
        public string Create(AccountInfo account)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    //need to add the rest. Only does logs and timestamp for now
                    var sqlStatement = string.Format("INSERT INTO accounts (email, passphrase, accountStatus, accountType) " +
                        "VALUES('{0}', '{1}', 'active' ,'{2}');", account._email, account._passphrase, account._accountStatus);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();

                    }
                    var savedSqlStatement = string.Format("select * from accounts where email = '{0}'", account._email);
                    using (var checkSave = new SqlCommand(savedSqlStatement, connection))
                    {
                        SqlDataReader reader = checkSave.ExecuteReader();

                        if (reader.HasRows)
                        {
                            connection.Close();
                            return "Successful Account Creation";
                        }
                        else
                        {
                            connection.Close();
                            return "Account was not saved onto the data store";
                        }
                    }

                }

            }
            catch (SqlException e)
            {
                return "Data Accces Layer error";
            }
        }

        public string Delete(AccountInfo account)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("delete from accounts where email = '{0}'", account._email);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    var savedSqlStatement = string.Format("select * from accounts where email = '{0}'", account._email);
                    using (var checkSave = new SqlCommand(savedSqlStatement, connection))
                    {
                        SqlDataReader reader = checkSave.ExecuteReader();

                        if (reader.HasRows)
                        {
                            connection.Close();
                            return "Unsuccessful Account Deletion";
                        }
                        else
                        {
                            connection.Close();
                            return "Account was deleted successfully";
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                return "Data Access Layer error.";
            }

        }

        public List<string> Read(AccountInfo account)
        {
            throw new NotImplementedException();
        }

        public string Update(AccountInfo account)
        {
            throw new NotImplementedException();
        }

        public string Disable(AccountInfo account)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("update accounts set accountStatus = 'disabled' where email = '{0}'", account._email);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    var savedSqlStatement = string.Format("select * from accounts where email = '{0}'", account._email);
                    using (var checkSave = new SqlCommand(savedSqlStatement, connection))
                    {
                        SqlDataReader reader = checkSave.ExecuteReader();

                        if (reader.HasRows)
                        {

                            var disabled = "";
                            while (reader.Read())
                            {
                                disabled = string.Format("{0}", reader["accountStatus"]);
                            }
                            if (disabled.Equals("disabled"))
                            {
                                connection.Close();
                                return "Account was disabled";
                            }
                            else
                            {
                                connection.Close();
                                return "Account was not disabled";
                            }
                        }
                        else
                        {
                            connection.Close();
                            return "Account not found.";
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                return "Data Access Layer error.";
            }
        }
        public string Enable(AccountInfo account)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("update accounts set accountStatus = 'active' where email = '{0}'", account._email);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    var savedSqlStatement = string.Format("select * from accounts where email = '{0}'", account._email);
                    using (var checkSave = new SqlCommand(savedSqlStatement, connection))
                    {
                        SqlDataReader reader = checkSave.ExecuteReader();

                        if (reader.HasRows)
                        {

                            var enabled = "";
                            while (reader.Read())
                            {
                                enabled = string.Format("{0}", reader["accountStatus"]);

                            }
                            if (enabled.Equals("active"))
                            {
                                connection.Close();
                                return "Account was enabled";
                            }
                            else
                            {
                                connection.Close();
                                return "Account was not enabled";
                            }
                        }
                        else
                        {
                            connection.Close();
                            return "Account not found.";
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                return "Data Access Layer error.";
            }
        }
        public string AuthnAccount(AccountInfo account)
        {
            throw new NotImplementedException();
        }
        public string AuthzAccount(AccountInfo account)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("select * from accounts where email = '{0}'", account._email);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {

                            var active = "";
                            var authorizedView = "";
                            while (reader.Read())
                            {
                                active = string.Format("{0}", reader["accountStatus"]);
                                authorizedView = string.Format("{0}", reader["accountType"]);

                            }
                            if (active.Equals("active"))
                            {
                                return authorizedView;
                            }
                            else
                            {
                                connection.Close();
                                return "Account is disabled";
                            }
                        }
                        else
                        {
                            connection.Close();
                            return "Account not found.";
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                return "Data Access Layer error.";
            }
        }
        public string getConnection()
        {
            //var SQLConnectionString = ConfigurationManager.AppSettings.Get("sqlConnectionString");
            var SQLConnectionString = @"Server=localhost\SQLEXPRESS;Database=UM;Trusted_Connection=True";
            return SQLConnectionString;
        }
    }
}
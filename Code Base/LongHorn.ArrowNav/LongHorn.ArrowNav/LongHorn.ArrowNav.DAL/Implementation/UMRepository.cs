using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace LongHorn.ArrowNav.DAL
{
    public class UMRepository : IRepository<AccountInfo>
    {
        /** My thought process is that everyone will be a user in the system and if need be we can change their accessLevel from the database. 
         * Just changed the Create method to the way i think it should be. As well as it is the only one that currently has the implementation of the new tables 
         * Also the possiblility of having another if statement which will first check if the account already exists. If it does the account creation is then cut short and returns 
         * that the account already exists.
         * TODO:
         * done 1) Write an if statement that checks beforehand if the account already exists in the database 
         * done 2) Update the SQL statements so that it reflects the current tables in the database(might do stored procedures in this method)
         * done 3) Check the logging repository to see if it needs any new changes due to the update AccountInfo model
         * 4) Test run the controller.
         * 5) Debug if you need to 
         */
        public string Create(AccountInfo account)
        {
            try
            {
                var sqlConnectionString = getConnection();
                

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    //checks if the account already exists
                    var checkAccountExistence = string.Format("exec GetUserByEmail '{0}' ", account._email);
                    using (var checkAccount = new SqlCommand(checkAccountExistence, connection))
                    {
                        SqlDataReader reader = checkAccount.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Close();
                            connection.Close();
                            return "Account already exists";
                        }
                        else
                        {
                            reader.Close();
                            var addAccount = string.Format("exec AddUser '{0}', '{1}'", account._email, account._passphrase);
                            using (var addCommand = new SqlCommand(addAccount, connection))
                            {
                                addCommand.ExecuteNonQuery();
                            }
                            var addProfile = string.Format("exec CreateProfile '{0}','{1}','{2}'", account._firstName, account._lastName,account._email);
                            using (var addProfileCommand = new SqlCommand(addProfile, connection))
                            {
                                addProfileCommand.ExecuteNonQuery();
                            }

                            var savedSqlStatement = string.Format("exec GetUserByEmail '{0}' ", account._email);
                            using (var checkSave = new SqlCommand(savedSqlStatement, connection))
                            {
                                SqlDataReader userReader = checkSave.ExecuteReader();

                                if (userReader.HasRows)
                                {
                                    userReader.Close();
                                    connection.Close();
                                    return "Successful Account Creation";
                                }
                                else
                                {
                                    userReader.Close();
                                    connection.Close();
                                    return "Account was not saved onto the data store";
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
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("update accounts set passphrase = '{0}', accountType = '{1}' where email = '{2}'", account._passphrase, "", account._email);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    var savedSqlStatement = string.Format("select * from accounts where email = '{0}'", account._email);
                    using (var checkSave = new SqlCommand(savedSqlStatement, connection))
                    {
                        SqlDataReader reader = checkSave.ExecuteReader();

                        return "Account Updated";
                    }
                }
            }
            catch (SqlException e)
            {
                return "Data Access Layer error.";
            }
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

        public string confirmUserEmail(string email)
        {
            try
            {
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var findEmail = string.Format("exec getUserByEmail '{0}'", email);
                    using (var doesEmailExist = new SqlCommand(findEmail, connection))
                    {
                        SqlDataReader reader = doesEmailExist.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Close();
                            var sqlCommand = string.Format("exec confirmUserEmail '{0}' ", email);
                            using (var command = new SqlCommand(sqlCommand, connection))
                            {
                                command.ExecuteNonQuery();
                            }
                            return "confirmed";
                        }
                        else 
                        {
                            reader.Close();
                            connection.Close();
                            var result = "The email is not registered to an account";
                            return result;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                return "Database Error";
            }

        }
        public string getConnection()
        {
            //var SQLConnectionString = ConfigurationManager.AppSettings.Get("UMsqlConnectionString");
            //var SQLConnectionString = @"Server=localhost\SQLEXPRESS;Database=UM;Trusted_Connection=True";
            var AzureConnectionString = @"Server=tcp:arrownav-db.database.windows.net,1433;Initial Catalog=ArrowNavDB;Persist Security Info=False;User ID=brayan_admin;Password=Bf040800;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return AzureConnectionString;
        }
    }
}
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

                            var createRewards = string.Format("exec InsertRewards '{0}'", account._email);
                            using (var command = new SqlCommand(createRewards, connection))
                            {
                                command.ExecuteNonQuery();
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

        public string Delete(string email)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec deleteUser '{0}'", email);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    var savedSqlStatement = string.Format("exec GetUserByEmail '{0}'",email);
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
                            return "account deleted";
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

        public List<User> getAllUsers()
        {
            List<User> retrievedValues = new List<User>();
            var sqlConnectionString = getConnection();
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                var sqlStatement = string.Format("exec GetAllUsers");
                using (var command = new SqlCommand(sqlStatement, connection))
                {
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            user.email = (string)reader["email"];
                            user.accessLevel = (string)reader["accessLevel"];
                            user.accountStatus = (string)reader["accountStatus"];
                            retrievedValues.Add(user);
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

        public string Update(User account)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec UpdateAccount '{0}','{1}','{2}'", account.accessLevel, account.accountStatus,account.email);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    var savedSqlStatement = string.Format("exec GetUserByEmail '{0}'", account.email);
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
                    var sqlStatement = string.Format("exec DisableAccountStatus '{0}'", account._email);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    var savedSqlStatement = string.Format("exec GetUserByEmail '{0}'", account._email);
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
                    var sqlStatement = string.Format("exec EnableAccountStatus '{0}'", account._email);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    var savedSqlStatement = string.Format("exec GetUserByEmail '{0}'", account._email);
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
        public string AuthnAccount(LoginModel model)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec GetUserByEmail '{0}'", model._Username);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {

                            var password = "";
                            while (reader.Read())
                            {
                                Console.WriteLine(reader["password"]);
                                password = string.Format("{0}", reader["password"]);

                            }
                            if (password.Equals(model._Password))
                            {
                                return "Account is authenticated";
                            }
                            else
                            {
                                connection.Close();
                                return "Incorrect Password";
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
        public string AuthzAccount(AccountInfo account)
        {
            try
            {
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec GetUserByEmail '{0}'", account._email);
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
                                authorizedView = string.Format("{0}", reader["accessLevel"]);

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

        public AccountInfo getProfile(string email)
        {
            try
            {
                AccountInfo accountInfo = new AccountInfo();
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var findEmail = string.Format("exec getProfileByEmail '{0}'", email);
                    using (var getProfile = new SqlCommand(findEmail, connection))
                    {
                        SqlDataReader rdr = getProfile.ExecuteReader();
                        while (rdr.Read())
                        {
                            accountInfo._email = email;
                            accountInfo._firstName = (string)rdr["firstName"];
                            accountInfo._lastName = (string)rdr["lastName"];
                        }
                        rdr.Close();
                        
                    }
                    connection.Close();
                }
                return accountInfo;
            }
            catch (SqlException e)
            {
                AccountInfo error = new AccountInfo();
                error._email = "error";
                return error;
            }
        }
        public string getConnection()
        {
            var AzureConnectionString = ConfigurationManager.AppSettings.Get("DatabaseString");
            return AzureConnectionString;
        }

        public string Delete(AccountInfo model)
        {
            throw new NotImplementedException();
        }

        public string Update(AccountInfo model)
        {
            throw new NotImplementedException();
        }
    }
}
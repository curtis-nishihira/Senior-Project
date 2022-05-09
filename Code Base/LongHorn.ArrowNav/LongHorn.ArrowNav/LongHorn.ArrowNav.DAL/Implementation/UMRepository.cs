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
                            var addProfile = string.Format("exec CreateProfile '{0}','{1}','{2}'", account._firstName, account._lastName, account._email);
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
                    var savedSqlStatement = string.Format("exec GetUserByEmail '{0}'", email);
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
                    var sqlStatement = string.Format("exec UpdateAccount '{0}','{1}','{2}'", account.accessLevel, account.accountStatus, account.email);
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
        public string UpdateSucessfulAttempt(string email)
        {

            try
            {
                var sqlConnectionString = getConnection();
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    SqlCommand updateDateAndAttempt = new SqlCommand("UpdateDateAndAttempts", connection);

                    //lets the SqlCommand Object know that its a store procedure type
                    updateDateAndAttempt.CommandType = System.Data.CommandType.StoredProcedure;

                    //adding the necessay parameters for the stored procedure
                    updateDateAndAttempt.Parameters.Add(new SqlParameter("@email", email));
                    updateDateAndAttempt.Parameters.Add(new SqlParameter("@date", ""));
                    updateDateAndAttempt.Parameters.Add(new SqlParameter("@attempts", 0));
                    
                    
                }

                return "updated";
            }
            catch (Exception)
            {
                return "DAL error"; 
            }


        }
        public string UpdateFailedAttempts(string email, string date)
        {

            try
            {
                var sqlConnectionString= getConnection();

                using( var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    SqlCommand getUser = new SqlCommand("GetUserByEmail", connection);

                    //lets the SqlCommand Object know that its a store procedure type
                    getUser.CommandType = System.Data.CommandType.StoredProcedure;

                    //adding the necessay parameters for the stored procedure
                    getUser.Parameters.Add(new SqlParameter("@EMAIL", email));

                    using (SqlDataReader reader = getUser.ExecuteReader())
                    {

                        if (reader.HasRows && reader.Read() == true)
                        {
                            var invalidAttempts = (int)reader["invalidAttempts"];
                            var dateString = (string)reader["DateFailedAttempt"];

                            if (invalidAttempts == 0)
                            {
                                reader.Close();
                                var updatedAttempts = invalidAttempts + 1;

                                SqlCommand updateDateAndAttempt = new SqlCommand("UpdateDateAndAttempts", connection);

                                //lets the SqlCommand Object know that its a store procedure type
                                updateDateAndAttempt.CommandType = System.Data.CommandType.StoredProcedure;

                                //adding the necessay parameters for the stored procedure
                                updateDateAndAttempt.Parameters.Add(new SqlParameter("@email", email));
                                updateDateAndAttempt.Parameters.Add(new SqlParameter("@date", date));
                                updateDateAndAttempt.Parameters.Add(new SqlParameter("@attempts", updatedAttempts));
                                try
                                {
                                    updateDateAndAttempt.ExecuteNonQuery();
                                    return "Failed OTP Attempt";
                                }
                                catch (Exception e)
                                {

                                    return e.Message;
                                }
                                
                            }
                            else if(invalidAttempts < 5)
                            {
                                reader.Close();
                                var todayDate = DateTime.Parse(date);
                                var date2 = DateTime.Parse(dateString);
                                TimeSpan difference = todayDate.Subtract(date2);
                                double totalHours = difference.TotalHours;
                                if(totalHours <= 24)
                                {
                                    var updatedAttempts = invalidAttempts + 1;
                                    SqlCommand updateAttempt = new SqlCommand("UpdateAttemptValue", connection);

                                    //lets the SqlCommand Object know that its a store procedure type
                                    updateAttempt.CommandType = System.Data.CommandType.StoredProcedure;

                                    //adding the necessay parameters for the stored procedure
                                    updateAttempt.Parameters.Add(new SqlParameter("@email", email));
                                    updateAttempt.Parameters.Add(new SqlParameter("@attempts", updatedAttempts));
                                }
                                else
                                {
                                    SqlCommand updateDateAndAttempt = new SqlCommand("UpdateAttemptValue", connection);

                                    //lets the SqlCommand Object know that its a store procedure type
                                    updateDateAndAttempt.CommandType = System.Data.CommandType.StoredProcedure;

                                    //adding the necessay parameters for the stored procedure
                                    updateDateAndAttempt.Parameters.Add(new SqlParameter("@email", email));
                                    updateDateAndAttempt.Parameters.Add(new SqlParameter("@date", date));
                                    updateDateAndAttempt.Parameters.Add(new SqlParameter("@attempts", 1));
                                }
                                return "Failed OTP Attempt";
                            }
                            else
                            {
                                reader.Close();
                                return "Too many attempts were made. Account has been disabled";
                            }

                        }
                        else
                        {
                            return "Account unable to update";
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                return "Server Error";
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

       
        public LoginResponse AuthnAccount(LoginModel model)
        {
            try
            {
                LoginResponse response = new LoginResponse();
                var sqlConnectionString = getConnection();

                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    var sqlStatement = string.Format("exec GetUserByEmail '{0}'", model.Username);
                    using (var command = new SqlCommand(sqlStatement, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            var password = "";
                            var isConfirmed = "";
                            var accessLevel = "";
                            var accountStatus = "";
                            while (reader.Read())
                            {
                                password = string.Format("{0}", reader["password"]);
                                isConfirmed = (string)reader["emailConfirmed"];
                                accessLevel = (string)reader["accessLevel"];
                                accountStatus = (string)reader["accountStatus"];

                            }

                            if (isConfirmed == "true")
                            {
                                if (password.Equals(model.Password))
                                {
                                   if(accountStatus == "active")
                                    {
                                        response.Message = "Account is authenticated";
                                        if (accessLevel.Contains("admin"))
                                        {
                                            response.IsAuthorized = true;
                                        }
                                    }
                                    else
                                    {
                                        response.Message = "Account Disabled";
                                    }
                                }
                                else
                                {
                                    response.Message = "Incorrect Password";
                                }
                            }
                            else
                            {
                                response.Message = "Email is not confirmed";
                            }
                            
                        }
                        else
                        {
                            response.Message = "Account not found.";
                        }

                    }
                    return response;
                }
            }
            catch (SqlException e)
            {
                LoginResponse error = new LoginResponse();
                error.Message = "Data Access Layer error.";
                return error;
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
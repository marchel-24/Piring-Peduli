using Npgsql;
using PiringPeduliClass.Model;
using System;
using System.Diagnostics;

namespace PiringPeduliClass.Repository
{
    public class AccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Account GetAccountById(int id)
        {
            Account account = null;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM account WHERE accountid = @accountid", connection))
                {
                    command.Parameters.AddWithValue("@accountid", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            account = new Account
                            {
                                AccountId = reader.GetInt32(reader.GetOrdinal("accountid")),
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                Type = (AccountType)Enum.Parse(typeof(AccountType), reader.GetString(reader.GetOrdinal("type")))
                            };
                        }
                    }
                }
            }

            return account;
        }

        public Account GetAccountByUsername(string username) {
            Account account = null;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM account WHERE username = @username", connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            account = new Account
                            {
                                AccountId = reader.GetInt32(reader.GetOrdinal("accountid")),
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                Type = (AccountType)Enum.Parse(typeof(AccountType), reader.GetString(reader.GetOrdinal("type")))
                            };
                        }
                    }
                }
            }

            return account;
        }

        public async Task<int> GetNextAccountIdAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();  // Use async version for opening the connection

                using (var command = new NpgsqlCommand("SELECT COALESCE(MAX(accountid), 0) + 1 FROM account", connection))
                {
                    // Use async version of ExecuteScalar
                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }
            }
        }

        public int GetIdFromUsername(string username)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT accountid FROM account WHERE username = @username", connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    // Execute the command and convert the result to int
                    object result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1; // Return -1 or another value if the user is not found
                }
            }
        }

        public async Task<bool> CreateAccountAsync(Account account)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();  // Use async version for opening the connection

                    using (var command = new NpgsqlCommand(
                        "INSERT INTO account (accountid, username, password, type) VALUES (@accountid, @username, @password, @type::account_type)", connection))
                    {
                        command.Parameters.AddWithValue("@accountid", account.AccountId);
                        command.Parameters.AddWithValue("@username", account.Username);
                        command.Parameters.AddWithValue("@password", account.Password);
                        command.Parameters.AddWithValue("@type", account.Type.ToString());

                        // Use async version of ExecuteNonQuery
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return true;  // Return true if account creation is successful
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., to a logging service or console)
                Debug.WriteLine($"Error creating account: {ex.Message}");

                return false;  // Return false if an error occurs
            }
        }


        public void UpdateAccount(Account account)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(
                    "UPDATE account SET username = @username, password = @password, type = @type::account_type WHERE accountid = @accountid", connection))
                {
                    command.Parameters.AddWithValue("@accountid", account.AccountId);
                    command.Parameters.AddWithValue("@username", account.Username);
                    command.Parameters.AddWithValue("@password", account.Password);
                    command.Parameters.AddWithValue("@type", account.Type.ToString());

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAccountById(int accountId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(
                    "DELETE FROM account WHERE accountid = @accountid", connection))
                {
                    command.Parameters.AddWithValue("@accountid", accountId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAccountByUsername(string username)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(
                    "DELETE FROM account WHERE username = @username", connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool ValidateAccount(string username, string password)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(
                    "SELECT COUNT(1) FROM account WHERE username = @username AND password = @password", connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    var result = (long)command.ExecuteScalar();

                    return result > 0;
                }
            }
        }
    }
}

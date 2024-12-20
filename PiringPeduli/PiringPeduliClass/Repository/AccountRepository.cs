﻿using Npgsql;
using PiringPeduliClass.Model;
using System;
using System.Diagnostics;

namespace PiringPeduliClass.Repository
{
    public class AccountRepository
    {
        protected readonly string _connectionString;

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
                                Type = (AccountType)Enum.Parse(typeof(AccountType), reader.GetString(reader.GetOrdinal("type"))),
                                Lat = (double?)(reader.IsDBNull(reader.GetOrdinal("lat")) ? null : reader.GetDouble(reader.GetOrdinal("lat"))) ?? 0.0,
                                Lon = (double?)(reader.IsDBNull(reader.GetOrdinal("lon")) ? null : reader.GetDouble(reader.GetOrdinal("lon"))) ?? 0.0
                            };
                        }
                    }
                }
            }

            return account;
        }

        public async Task<Account> GetAccountByUsernameAsync(string username)
        {
            Account account = null;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();  // Use async version for opening the connection

                using (var command = new NpgsqlCommand("SELECT * FROM account WHERE username = @username", connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    using (var reader = await command.ExecuteReaderAsync())  // Use async version for ExecuteReader
                    {
                        if (await reader.ReadAsync())  // Use async version for Read
                        {
                            account = new Account
                            {
                                AccountId = reader.GetInt32(reader.GetOrdinal("accountid")),
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                Lat = (double?)(reader.IsDBNull(reader.GetOrdinal("lat")) ? null : reader.GetDouble(reader.GetOrdinal("lat"))) ?? 0.0,
                                Lon = (double?)(reader.IsDBNull(reader.GetOrdinal("lon")) ? null : reader.GetDouble(reader.GetOrdinal("lon"))) ?? 0.0,
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

        public async Task<int> GetIdFromUsernameAsync(string username)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("SELECT accountid FROM account WHERE username = @username", connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    // Execute the command asynchronously and convert the result to int
                    object result = await command.ExecuteScalarAsync();
                    return result != null ? Convert.ToInt32(result) : -1; // Return -1 if the user is not found
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
                        command.Parameters.AddWithValue("@lat", account.Lat);
                        command.Parameters.AddWithValue("@lon", account.Lon);
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


        public async Task<bool> UpdateAccountAsync(Account account)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand(
                        "UPDATE account SET username = @username, password = @password, type = @type::account_type, lat = @lat, lon = @lon WHERE accountid = @accountid", connection))
                    {
                        command.Parameters.AddWithValue("@accountid", account.AccountId);
                        command.Parameters.AddWithValue("@username", account.Username);
                        command.Parameters.AddWithValue("@password", account.Password);
                        command.Parameters.AddWithValue("@type", account.Type.ToString());
                        command.Parameters.AddWithValue("@lat", account.Lat);
                        command.Parameters.AddWithValue("@lon", account.Lon);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., to a logging service or console)
                Debug.Print($"Error updating account: {ex.Message}");

                return false;  // Return false if an error occurs
            }
        }


        public async Task<bool> DeleteAccountById(int accountId)
        {
            try
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public async Task<bool> DeleteAccountByUsernameAsync(string username)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("DELETE FROM account WHERE username = @username", connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting account: {ex.Message}");

                return false;  // Return false if an error occurs
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

        // Method to get all accounts
        public async Task<List<Account>> GetAllAccountsTemporaryStorageAsync()
        {
            var accounts = new List<Account>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string query = "SELECT accountid, username, password, type, lat, lon FROM account WHERE type = 'TemporaryStorage'";

                    using (var command = new NpgsqlCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var account = new Account
                            {
                                AccountId = reader.GetInt32(reader.GetOrdinal("accountid")),
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                Type = (AccountType)Enum.Parse(typeof(AccountType), reader.GetString(reader.GetOrdinal("type"))),
                                Lat = (double?)(reader.IsDBNull(reader.GetOrdinal("lat")) ? null : reader.GetDouble(reader.GetOrdinal("lat"))) ?? 0.0,
                                Lon = (double?)(reader.IsDBNull(reader.GetOrdinal("lon")) ? null : reader.GetDouble(reader.GetOrdinal("lon"))) ?? 0.0
                            };

                            accounts.Add(account);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving accounts: {ex.Message}");
            }

            return accounts;
        }
    }
}

using Npgsql;
using PiringPeduliClass.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliClass.Repository
{
    public class TemporaryStorageRepository:AccountRepository
    {
        public TemporaryStorageRepository(string connectionstring) : base(connectionstring) { }

        public async Task<bool> AddTemporaryStorageAsync(TemporaryStorage account)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand
                        (
                            "INSERT INTO temporarystorage (storageid, storageaddress, accountid, storagename) VALUES (DEFAULT, @storageaddress, @accountid, @storagename)", connection
                            )
                        )
                    {
                        command.Parameters.AddWithValue("@storageaddress", account.StorageAddress);
                        command.Parameters.AddWithValue("@accountid", account.AccountId);
                        command.Parameters.AddWithValue("@storagename", account.StorageName);

                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating courier: {ex.Message}");

                return false;
            }
            
        }

        public async Task<bool> UpdateTemporaryStorageAsync(TemporaryStorage temporaryStorage)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string query = "UPDATE temporarystorage SET storageaddress = @storageaddress, storagename = @storagename WHERE accountid = @accountid";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accountid", temporaryStorage.AccountId);
                        command.Parameters.AddWithValue("@storageaddress", temporaryStorage.StorageAddress);
                        command.Parameters.AddWithValue("@storagename", temporaryStorage.StorageName);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error updating storage: {ex.Message}");
                return false;
            }
        }

        public async Task<List<TemporaryStorage>> GetAllTemporaryStorageAsync()
        {
            var temporaryStorageList = new List<TemporaryStorage>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string query = @"
                    SELECT 
                        ts.storageid,
                        ts.storageaddress,
                        ts.accountid,
                        ts.storagename,
                        a.username,
                        a.password,
                        a.type,
                        a.lat,
                        a.lon
                    FROM 
                        temporarystorage ts
                    INNER JOIN 
                        account a
                    ON 
                        ts.accountid = a.accountid";

                    using (var command = new NpgsqlCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var temporaryStorage = new TemporaryStorage
                            {
                                StorageId = reader.GetInt32(0),
                                StorageAddress = reader.GetString(1),
                                AccountId = reader.GetInt32(2),
                                StorageName = reader.GetString(3),
                                Username = reader.GetString(4),
                                Password = reader.GetString(5),
                                Type = (AccountType)Enum.Parse(typeof(AccountType), reader.GetString(6)),
                                Lat = reader.GetDouble(7),
                                Lon = reader.GetDouble(8)
                            };

                            temporaryStorageList.Add(temporaryStorage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"Error fetching temporary storage with account: {ex.Message}");
            }

            return temporaryStorageList;
        }


    }
}

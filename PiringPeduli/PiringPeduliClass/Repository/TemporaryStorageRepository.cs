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

        public void makeAccount(TemporaryStorage account)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand
                    (
                        "INSERT INTO temporarystorage (storageid, storageaddress, accountid, storagename) VALUES (DEFAULT, @storageaddress, @accountid, @storagename)", connection
                        )
                    )
                {
                    //command.Parameters.AddWithValue("@storageid", account.StorageId);
                    command.Parameters.AddWithValue("@storageaddress", account.StorageAddress);
                    command.Parameters.AddWithValue("@accountid", account.AccountId);
                    command.Parameters.AddWithValue("@storagename", account.StorageName);

                    command.ExecuteNonQuery();
                }
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
    }
}

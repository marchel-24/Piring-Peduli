using Npgsql;
using PiringPeduliClass.Model;
using System;
using System.Collections.Generic;
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
                        "INSERT INTO temporarystorage (storageid, storageaddress, accountid) VALUES (DEFAULT, @storageaddress, @accountid)", connection
                        )
                    )
                {
                    //command.Parameters.AddWithValue("@storageid", account.StorageId);
                    command.Parameters.AddWithValue("@storageaddress", account.StorageAddress);
                    command.Parameters.AddWithValue("@accountid", account.AccountId);

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
                    string query = "UPDATE temporarystorage SET storageaddress = @storageaddress WHERE accountid = @accountid";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accountid", temporaryStorage.AccountId);
                        command.Parameters.AddWithValue("@storageaddress", temporaryStorage.StorageAddress);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating storage: {ex.Message}");
                return false;
            }
        }
    }
}

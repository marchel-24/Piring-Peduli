using Npgsql;
using PiringPeduliClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliClass.Repository
{
    public class TemporaryStorageRepository
    {
        private readonly string _connectionString;

        public TemporaryStorageRepository (string connectionString)
        {
            _connectionString = connectionString;
        }

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
    }
}

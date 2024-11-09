using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using PiringPeduliClass.Model;

namespace PiringPeduliClass.Repository
{
    public class WasteInStorageRepository
    {
        private readonly string _connectionString;

        public WasteInStorageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public WasteInStorage GetWasteByStorage(int storageId)
        {
            WasteInStorage wasteStorage = null;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM wasteinstorage WHERE storageid = @storageid", connection))
                {
                    command.Parameters.AddWithValue("@storageid", storageId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            wasteStorage = new WasteInStorage
                            {
                                Wasteid = reader.GetInt32(reader.GetOrdinal("wasteid")),
                                Storageid = reader.GetInt32(reader.GetOrdinal("storageid")),
                                Quantity = reader.GetDouble(reader.GetOrdinal("quantity"))
                            };
                        }
                    }
                }
            }

            return wasteStorage;
        }

        public void AddWasteToStorage(WasteInStorage wasteInStorage)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("INSERT INTO wasteinstorage (wasteid, storageid, quantity) VALUES (@wasteid, @storageid, @quantity)", connection))
                {
                    command.Parameters.AddWithValue("@wasteid", wasteInStorage.Wasteid);
                    command.Parameters.AddWithValue("@storageid", wasteInStorage.Storageid);
                    command.Parameters.AddWithValue("@quantity", wasteInStorage.Quantity);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}

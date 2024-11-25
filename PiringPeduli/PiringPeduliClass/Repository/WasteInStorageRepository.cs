using System.Collections.Generic;
using System.Diagnostics;
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

        public WasteInStorage? GetWasteByStorageAndWaste(int storageId, int wasteId)
        {
            WasteInStorage? wasteStorage = null;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM wasteinstorage WHERE storageid = @storageid AND wasteid = @wasteid", connection))
                {
                    command.Parameters.AddWithValue("@storageid", storageId);
                    command.Parameters.AddWithValue("@wasteid", wasteId);

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

        public List<WasteInStorage> GetWasteByStorage(int storageId)
        {
            List<WasteInStorage> wasteStorage = new List<WasteInStorage>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(@"SELECT 
                                                            ws.storageid,
                                                            ws.wasteid,
                                                            ws.quantity,
                                                            sw.wastetype
                                                        FROM 
                                                            public.wasteinstorage ws
                                                        JOIN 
                                                            public.sortedwaste sw
                                                        ON 
                                                            ws.wasteid = sw.wasteid
                                                        WHERE
                                                            ws.storageid = @storageid;", connection))
                {
                    command.Parameters.AddWithValue("@storageid", storageId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            wasteStorage.Add(new WasteInStorage
                            {
                                Wasteid = reader.GetInt32(reader.GetOrdinal("wasteid")),
                                Storageid = reader.GetInt32(reader.GetOrdinal("storageid")),
                                Quantity = reader.GetDouble(reader.GetOrdinal("quantity")),
                                WasteType = reader.GetString(reader.GetOrdinal("wastetype"))
                            });

                            
                        }
                    }
                }
            }

            return wasteStorage;
        }

        public List<WasteInStorage> GetAllWaste()
        {
            List<WasteInStorage> wasteStorage = new List<WasteInStorage>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(@"SELECT 
                                                            ws.storageid,
                                                            ts.storagename,
                                                            ws.wasteid,
                                                            ws.quantity,
                                                            sw.wastetype
                                                        FROM 
                                                            public.wasteinstorage ws
                                                        JOIN 
                                                            public.sortedwaste sw
                                                        ON 
                                                            ws.wasteid = sw.wasteid
                                                        JOIN 
                                                            public.temporarystorage ts
                                                        ON 
                                                            ws.storageid = ts.accountid;", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            wasteStorage.Add(new WasteInStorage
                            {
                                Wasteid = reader.GetInt32(reader.GetOrdinal("wasteid")),
                                Storageid = reader.GetInt32(reader.GetOrdinal("storageid")),
                                Quantity = reader.GetDouble(reader.GetOrdinal("quantity")),
                                WasteType = reader.GetString(reader.GetOrdinal("wastetype")),
                                StorageName = reader.GetString(reader.GetOrdinal("storagename"))
                            });


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

        public void DeleteWasteByStorageAndWaste(int storageId, int wasteId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("DELETE FROM wasteinstorage WHERE storageid = @storageid AND wasteid = @wasteid", connection))
                {
                    command.Parameters.AddWithValue("@storageid", storageId);
                    command.Parameters.AddWithValue("@wasteid", wasteId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateWasteInStorage(WasteInStorage wasteInStorage)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("UPDATE wasteinstorage SET quantity = @quantity WHERE wasteid = @wasteid AND storageid = @storageid", connection))
                {
                    command.Parameters.AddWithValue("@quantity", wasteInStorage.Quantity);
                    command.Parameters.AddWithValue("@wasteid", wasteInStorage.Wasteid);
                    command.Parameters.AddWithValue("@storageid", wasteInStorage.Storageid);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}

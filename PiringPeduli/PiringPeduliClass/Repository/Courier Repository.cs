using Npgsql;
using PiringPeduliClass.Model;
using System;
using System.Diagnostics;

namespace PiringPeduliClass.Repository
{
    public class CourierRepository : AccountRepository
    {
        // Constructor uses the base class constructor to initialize the connection string
        public CourierRepository(string connectionString) : base(connectionString) { }

        public async Task<bool> AddCourierAsync(Courier courier)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = "INSERT INTO courier (CourierId, couriername, VehicleType, accountid) VALUES (DEFAULT, @Name, @VehicleType::vehicle_type, @AccountID)";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", courier.Name);
                        command.Parameters.AddWithValue("@VehicleType", courier.Vehicle.ToString());
                        command.Parameters.AddWithValue("@AccountID", courier.AccountId);

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

        public Courier GetCourierById(int courierId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = "SELECT * FROM courier WHERE courierid = @courierid";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@courierid", courierId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Courier
                            {
                                CourierId = reader.GetInt32(reader.GetOrdinal("courierid")),
                                AccountId = reader.GetInt32(reader.GetOrdinal("accountid")),
                                Name = reader.GetString(reader.GetOrdinal("couriername")),
                                Vehicle = (VehicleType)Enum.Parse<VehicleType>(reader.GetString(reader.GetOrdinal("vehicletype")))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public Courier GetCourierByName(string couriername)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = "SELECT * FROM courier WHERE couriername = @couriername";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@couriername", couriername);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Courier
                            {
                                CourierId = reader.GetInt32(reader.GetOrdinal("courierid")),
                                AccountId = reader.GetInt32(reader.GetOrdinal("accountid")),
                                Name = reader.GetString(reader.GetOrdinal("couriername")),
                                Vehicle = (VehicleType)Enum.Parse<VehicleType>(reader.GetString(reader.GetOrdinal("vehicletype")))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task<bool> UpdateCourierAsync(Courier courier)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string query = "UPDATE courier SET couriername = @couriername, vehicletype = @vehicletype::vehicle_type WHERE accountid = @accountid";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accountid", courier.AccountId);
                        command.Parameters.AddWithValue("@couriername", courier.Name);
                        command.Parameters.AddWithValue("@vehicletype", courier.Vehicle.ToString());

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error updating courier: {ex.Message}");
                return false;
            }
        }

        public void DeleteCourier(int courierId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = "DELETE FROM courier WHERE courierid = @courierid";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@courierid", courierId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
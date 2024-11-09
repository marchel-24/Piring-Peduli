using Npgsql;
using PiringPeduliClass.Model;
using System;

namespace PiringPeduliClass.Repository
{
    public class CourierRepository
    {
        private readonly string _connectionString;

        public CourierRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddCourier(Courier courier)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = "INSERT INTO courier (CourierId, couriername, VehicleType, accountid) VALUES (DEFAULT, @Name, @VehicleType::vehicle_type, @AccountID)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@CourierId", courier.CourierId);
                    command.Parameters.AddWithValue("@Name", courier.Name);
                    command.Parameters.AddWithValue("@VehicleType", courier.Vehicle.ToString());
                    command.Parameters.AddWithValue("@AccountID", courier.AccountId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
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

        public void UpdateCourier(Courier courier)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = "UPDATE courier SET couriername = @couriername, vehicletype = @vehicletype::vehicle_type WHERE courierid = @courierid";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@courierid", courier.CourierId);
                    command.Parameters.AddWithValue("@couriername", courier.Name);
                    command.Parameters.AddWithValue("@vehicletype", courier.Vehicle.ToString());

                    connection.Open();
                    command.ExecuteNonQuery();
                }
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

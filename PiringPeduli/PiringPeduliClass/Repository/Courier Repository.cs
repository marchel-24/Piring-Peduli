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
                string query = "SELECT * FROM Couriers WHERE CourierId = @CourierId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourierId", courierId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Courier
                            {
                                CourierId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Vehicle = Enum.Parse<VehicleType>(reader.GetString(2))
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
                string query = "UPDATE Couriers SET Name = @Name, VehicleType = @VehicleType WHERE CourierId = @CourierId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourierId", courier.CourierId);
                    command.Parameters.AddWithValue("@Name", courier.Name);
                    command.Parameters.AddWithValue("@VehicleType", courier.Vehicle.ToString());

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCourier(int courierId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = "DELETE FROM Couriers WHERE CourierId = @CourierId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourierId", courierId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

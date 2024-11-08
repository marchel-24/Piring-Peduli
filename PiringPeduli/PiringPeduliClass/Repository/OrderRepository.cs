using Npgsql;
using PiringPeduliClass.Model;
using System;
using System.Collections.Generic;

namespace PiringPeduliClass.Repository
{
    public class OrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddOrder(Order order)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = "INSERT INTO Orders (orderid, status, sourceid, destinationid, courierid, Description) " +
                               "VALUES (@OrderId, @Status::status, @SourceAccountId, @DestinationAccountId, @courierAccountId, @Description)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", order.OrderId);
                    command.Parameters.AddWithValue("@Status", order.Status.ToString());
                    command.Parameters.AddWithValue("@SourceAccountId", order.Source);
                    command.Parameters.AddWithValue("@DestinationAccountId", order.Destination);
                    command.Parameters.AddWithValue("@courierAccountId", order.Courier);
                    command.Parameters.AddWithValue("@Description", order.Description);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateOrder(Order order)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = "UPDATE Orders SET " +
                               "status = @Status::status, " +
                               "sourceid = @SourceAccountId, " +
                               "destinationid = @DestinationAccountId, " +
                               "courierid = @CourierAccountId, " +
                               "Description = @Description " +
                               "WHERE orderid = @OrderId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", order.OrderId);
                    command.Parameters.AddWithValue("@Status", order.Status.ToString());
                    command.Parameters.AddWithValue("@SourceAccountId", order.Source);
                    command.Parameters.AddWithValue("@DestinationAccountId", order.Destination);
                    command.Parameters.AddWithValue("@CourierAccountId", order.Courier);
                    command.Parameters.AddWithValue("@Description", order.Description);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteOrder(int orderId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = "DELETE FROM Orders WHERE orderid = @OrderId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}

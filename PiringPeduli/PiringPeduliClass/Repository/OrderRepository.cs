using Npgsql;
using PiringPeduliClass.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PiringPeduliClass.Repository
{
    public class OrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> AddOrderCustomerAsync(Order order)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync(); // Use async version for opening the connection

                    string query = "INSERT INTO Orders (status, sourceid, destinationid, courierid, description, size) " +
                                   "VALUES (@Status::status, @SourceAccountId, @DestinationAccountId, @CourierAccountId, @Description, @Size::size)";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", order.Status.ToString());
                        command.Parameters.AddWithValue("@Size", order.Size.ToString());
                        command.Parameters.AddWithValue("@SourceAccountId", order.Source);
                        command.Parameters.AddWithValue("@DestinationAccountId", order.Destination);

                        // Handle null for courierAccountId
                        command.Parameters.AddWithValue("@CourierAccountId", DBNull.Value);
                        command.Parameters.AddWithValue("@Description", order.Description);

                        // Use async version of ExecuteNonQuery
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return true; // Return true if the order was added successfully
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace Debug.WriteLine with your logging framework)
                Debug.WriteLine($"Error adding order: {ex.Message}");

                return false; // Return false if an error occurs
            }
        }

        public void AddOrderRecycler(Order order)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open(); // Use async version for opening the connection

                string query = "INSERT INTO Orders (status, sourceid, destinationid, courierid, description, size, quantity) " +
                               "VALUES (@Status::status, @SourceAccountId, @DestinationAccountId, @CourierAccountId, @Description, @Size::size, @quantity)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", order.Status.ToString());
                    command.Parameters.AddWithValue("@Size", order.Size.ToString());
                    command.Parameters.AddWithValue("@SourceAccountId", order.Source);
                    command.Parameters.AddWithValue("@quantity", order.Quantity);
                    command.Parameters.AddWithValue("@DestinationAccountId", order.Destination);

                    // Handle null for courierAccountId
                    command.Parameters.AddWithValue("@CourierAccountId", DBNull.Value);
                    command.Parameters.AddWithValue("@Description", order.Description);

                    Debug.WriteLine("IKI");

                    // Use async version of ExecuteNonQuery
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }



        public void UpdateOrder(Order order)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = "UPDATE Orders SET " +
                               "status = @Status::status, " +
                               "courierid = @CourierAccountId " +
                               "WHERE orderid = @OrderId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", order.OrderId);
                    command.Parameters.AddWithValue("@Status", order.Status.ToString());
                    if(order.Courier != null)
                    {
                        command.Parameters.AddWithValue("@CourierAccountId", order.Courier);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@CourierAccountId", DBNull.Value);
                    }
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

        public List<Order> GetAllDetailedOrdersProcessing()
        {
            var orders = new List<Order>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = @"
            SELECT 
                o.orderid,
                o.status,
                o.sourceid,
                COALESCE(s.storageaddress, c.customeraddress, r.recycleraddress) AS source_address,
                o.destinationid,
                COALESCE(s2.storageaddress, c2.customeraddress, r2.recycleraddress) AS destination_address,
                o.courierid,
                o.description,
                o.size
            FROM 
                public.orders o
            LEFT JOIN public.temporarystorage s ON o.sourceid = s.accountid
            LEFT JOIN public.customer c ON o.sourceid = c.accountid
            LEFT JOIN public.recycler r ON o.sourceid = r.accountid
            LEFT JOIN public.temporarystorage s2 ON o.destinationid = s2.accountid
            LEFT JOIN public.customer c2 ON o.destinationid = c2.accountid
            LEFT JOIN public.recycler r2 ON o.destinationid = r2.accountid
            WHERE o.status = 'Processing';";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Order
                            {
                                OrderId = reader.GetInt32(0),
                                Status = (StatusType)Enum.Parse(typeof(StatusType), reader.GetString(1)),
                                Source = reader.GetInt32(2),
                                SourceAddress = reader.GetString(3),
                                Destination = reader.GetInt32(4),
                                DestinationAddress = reader.GetString(5),
                                Courier = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                                Description = reader.GetString(7),
                                Size = (Sizes)Enum.Parse(typeof(Sizes), reader.GetString(8))
                            });
                        }
                    }
                }
            }

            return orders;
        }

        public List<Order> GetAllDetailedOrders()
        {
            var orders = new List<Order>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = @"
            SELECT 
                o.orderid,
                o.status,
                o.sourceid,
	            COALESCE(s.storagename, c.customername, r.recyclername) AS source_name,
                COALESCE(s.storageaddress, c.customeraddress, r.recycleraddress) AS source_address,
                o.destinationid,
                COALESCE(s2.storagename, c2.customername, r2.recyclername) AS destination_name,
                COALESCE(s2.storageaddress, c2.customeraddress, r2.recycleraddress) AS destination_address,
                o.courierid,
                o.description,
                o.size
            FROM 
                public.orders o
            LEFT JOIN public.temporarystorage s ON o.sourceid = s.accountid
            LEFT JOIN public.customer c ON o.sourceid = c.accountid
            LEFT JOIN public.recycler r ON o.sourceid = r.accountid
            LEFT JOIN public.temporarystorage s2 ON o.destinationid = s2.accountid
            LEFT JOIN public.customer c2 ON o.destinationid = c2.accountid
            LEFT JOIN public.recycler r2 ON o.destinationid = r2.accountid;";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Order
                            {
                                OrderId = reader.GetInt32(0),
                                Status = (StatusType)Enum.Parse(typeof(StatusType), reader.GetString(1)),
                                Source = reader.GetInt32(2),
                                SourceName = reader.GetString(3),
                                SourceAddress = reader.GetString(4),
                                Destination = reader.GetInt32(5),
                                DestinationName = reader.GetString(6),
                                DestinationAddress = reader.GetString(7),
                                Courier = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                Description = reader.GetString(9),
                                Size = (Sizes)Enum.Parse(typeof(Sizes), reader.GetString(10))
                            });
                        }
                    }
                }
            }

            return orders;
        }


        public Order? GetOrderByCourierId(int courierId)
        {
            Order? order = null;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = @"
            SELECT 
                o.orderid,
                o.status,
                o.sourceid,
                COALESCE(s.storageaddress, c.customeraddress, r.recycleraddress) AS source_address,
                o.destinationid,
                COALESCE(s2.storageaddress, c2.customeraddress, r2.recycleraddress) AS destination_address,
                o.courierid,
                o.description,
                o.size
            FROM 
                public.orders o
            LEFT JOIN public.temporarystorage s ON o.sourceid = s.accountid
            LEFT JOIN public.customer c ON o.sourceid = c.accountid
            LEFT JOIN public.recycler r ON o.sourceid = r.accountid
            LEFT JOIN public.temporarystorage s2 ON o.destinationid = s2.accountid
            LEFT JOIN public.customer c2 ON o.destinationid = c2.accountid
            LEFT JOIN public.recycler r2 ON o.destinationid = r2.accountid
            WHERE o.courierid = @courierid
                AND o.status = 'Confirmed';
        ";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@courierid", courierId);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            order = new Order
                            {
                                OrderId = reader.GetInt32(0),
                                Status = (StatusType)Enum.Parse(typeof(StatusType), reader.GetString(1)),
                                Source = reader.GetInt32(2),
                                SourceAddress = reader.GetString(3),
                                Destination = reader.GetInt32(4),
                                DestinationAddress = reader.GetString(5),
                                Courier = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                                Description = reader.GetString(7),
                                Size = (Sizes)Enum.Parse(typeof(Sizes), reader.GetString(8))
                            };
                        }
                    }
                }
            }

            return order;
        }


        public List<Order> GetOrdersBySourceId(int sourceId)
        {
            var orders = new List<Order>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = "SELECT orderid, status, sourceid, destinationid, courierid, Description FROM orders WHERE sourceid = @sourceid";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sourceid", sourceId);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Order
                            {
                                OrderId = reader.GetInt32(0),
                                //(SortedType)Enum.Parse(typeof(SortedType), reader.GetString(reader.GetOrdinal("wastedtype"))
                                Status = (StatusType)Enum.Parse(typeof(StatusType), reader.GetString(reader.GetOrdinal("status"))),
                                Source = reader.GetInt32(2),
                                Destination = reader.GetInt32(3),
                                //Courier = reader.GetInt32(4),
                                Description = reader.GetString(5)
                            });
                        }
                    }
                }
            }

            return orders;
        }
    }
}

using Npgsql;
using PiringPeduliClass.Model;
using System;

namespace PiringPeduliClass.Repository
{
    public class CustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddCustomer(Customer customer)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("INSERT INTO Customer (customerid, customername, customerinstance, customeraddress, accountid) VALUES (@CustomerId, @CustomerName, @CustomerInstance, @CustomerAddress, @AccountId)", connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                    command.Parameters.AddWithValue("@CustomerInstance", customer.CustomerInstance);
                    command.Parameters.AddWithValue("@CustomerAddress", customer.CustomerAddress);
                    command.Parameters.AddWithValue("@AccountId", customer.AccountId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCustomerByName(string customerName, Customer updatedCustomer)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE Customer SET customerinstance = @CustomerInstance, customeraddress = @CustomerAddress, accountid = @AccountId WHERE customername = @CustomerName";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerInstance", updatedCustomer.CustomerInstance);
                    command.Parameters.AddWithValue("@CustomerAddress", updatedCustomer.CustomerAddress);
                    command.Parameters.AddWithValue("@AccountId", updatedCustomer.AccountId);
                    command.Parameters.AddWithValue("@CustomerName", customerName);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCustomerByName(string customerName)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Customer WHERE customername = @CustomerName";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerName", customerName);

                    command.ExecuteNonQuery();
                }
            }
        }

        public Customer GetCustomerByName(string customerName)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Customer WHERE customername = @CustomerName";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerName", customerName);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerId = reader.GetInt32(reader.GetOrdinal("customerid")),
                                CustomerName = reader.GetString(reader.GetOrdinal("customername")),
                                CustomerInstance = reader.GetString(reader.GetOrdinal("customerinstance")),
                                CustomerAddress = reader.GetString(reader.GetOrdinal("customeraddress")),
                                AccountId = reader.GetInt32(reader.GetOrdinal("accountid"))
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}

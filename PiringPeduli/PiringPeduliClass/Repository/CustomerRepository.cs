using Npgsql;
using PiringPeduliClass.Model;
using System;
using System.Diagnostics;

namespace PiringPeduliClass.Repository
{
    public class CustomerRepository : AccountRepository
    {
        public CustomerRepository(string connectionString) : base(connectionString) { }

        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand("INSERT INTO Customer (customerid, customername, customerinstance, customeraddress, accountid) VALUES (DEFAULT, @CustomerName, @CustomerInstance, @CustomerAddress, @AccountId)", connection))
                    {
                        command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                        command.Parameters.AddWithValue("@CustomerInstance", customer.CustomerInstance);
                        command.Parameters.AddWithValue("@CustomerAddress", customer.CustomerAddress);
                        command.Parameters.AddWithValue("@AccountId", customer.AccountId);

                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating customer: {ex.Message}");

                return false;
            }
        }

        public async Task<bool> UpdateCustomerAsync(Customer updatedCustomer)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Customer SET customername = @CustomerName, customerinstance = @CustomerInstance, customeraddress = @CustomerAddress WHERE accountid = @accountid";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerName", updatedCustomer.CustomerName);
                        command.Parameters.AddWithValue("@CustomerInstance", updatedCustomer.CustomerInstance);
                        command.Parameters.AddWithValue("@CustomerAddress", updatedCustomer.CustomerAddress);
                        command.Parameters.AddWithValue("@CustomerName", updatedCustomer.CustomerName);
                        command.Parameters.AddWithValue("@accountid", updatedCustomer.AccountId);

                        command.ExecuteNonQuery();
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

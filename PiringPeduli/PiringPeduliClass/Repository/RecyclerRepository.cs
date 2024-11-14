using Npgsql;
using PiringPeduliClass.Model;
using System.Collections.Generic;
using System.Diagnostics;

namespace PiringPeduliClass.Repository
{
    public class RecyclerRepository:AccountRepository
    {
        public RecyclerRepository(string connectionString) : base(connectionString) { }

        public void AddRecycler(Recycler recycler)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Recycler (recyclerid, recyclername, recycleraddress, accountid) " +
                               "VALUES (@RecyclerId, @RecyclerName, @RecyclerAddress, @AccountID)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RecyclerId", recycler.RecyclerId);
                    command.Parameters.AddWithValue("@RecyclerName", recycler.RecyclerName);
                    command.Parameters.AddWithValue("@RecyclerAddress", recycler.RecyclerAddress);
                    command.Parameters.AddWithValue("@AccountID", recycler.AccountId);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method to update recycler details by name
        public async Task<bool> UpdateRecyclerAsync(Recycler recycler)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string query = "UPDATE recycler SET recyclername = @recyclername, recycleraddress = @recycleraddress WHERE accountid = @accountid";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accountid", recycler.AccountId);
                        command.Parameters.AddWithValue("@recyclername", recycler.RecyclerName);
                        command.Parameters.AddWithValue("@recycleraddress", recycler.RecyclerAddress);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error updating recycler: {ex.Message}");
                return false;
            }
        }

        // Method to delete a recycler by name
        public void DeleteRecyclerByName(string recyclerName)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Recycler WHERE recyclername = @RecyclerName";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RecyclerName", recyclerName);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method to get a recycler by name
        public Recycler GetRecyclerByName(string recyclerName)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Recycler WHERE recyclername = @RecyclerName";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RecyclerName", recyclerName);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Recycler
                            {
                                RecyclerId = reader.GetInt32(reader.GetOrdinal("recyclerid")),
                                RecyclerName = reader.GetString(reader.GetOrdinal("recyclername")),
                                RecyclerAddress = reader.GetString(reader.GetOrdinal("recycleraddress")),
                                AccountId = reader.GetInt32(reader.GetOrdinal("accountid"))
                            };
                        }
                    }
                }
            }
            return null; // Return null if recycler not found
        }
    }
}

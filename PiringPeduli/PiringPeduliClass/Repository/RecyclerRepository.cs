using Npgsql;
using PiringPeduliClass.Model;
using System.Collections.Generic;

namespace PiringPeduliClass.Repository
{
    public class RecyclerRepository
    {
        private readonly string _connectionString;

        public RecyclerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

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
        public void UpdateRecyclerByName(string recyclerName, Recycler updatedRecycler)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE Recycler SET recycleraddress = @RecyclerAddress, accountid = @AccountID WHERE recyclername = @RecyclerName";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RecyclerAddress", updatedRecycler.RecyclerAddress);
                    command.Parameters.AddWithValue("@AccountID", updatedRecycler.AccountId);
                    command.Parameters.AddWithValue("@RecyclerName", recyclerName);

                    command.ExecuteNonQuery();
                }
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

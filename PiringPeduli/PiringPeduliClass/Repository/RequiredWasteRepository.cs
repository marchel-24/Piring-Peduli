using System.Collections.Generic;
using Npgsql;
using PiringPeduliClass.Model;

namespace PiringPeduliClass.Repository
{
    public class RequiredWasteRepository
    {
        private readonly string _connectionString;

        public RequiredWasteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddRequiredWaste(int recyclerId, int wasteId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO requiredwaste (recyclerid, wasteid) VALUES (@recyclerid, @wasteid)", connection))
                {
                    command.Parameters.AddWithValue("@recyclerid", recyclerId);
                    command.Parameters.AddWithValue("@wasteid", wasteId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRequiredWaste(int recyclerId, int wasteId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("DELETE FROM requiredwaste WHERE recyclerid = @recyclerid AND wasteid = @wasteid", connection))
                {
                    command.Parameters.AddWithValue("@recyclerid", recyclerId);
                    command.Parameters.AddWithValue("@wasteid", wasteId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

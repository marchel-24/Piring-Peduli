using Npgsql;
using PiringPeduliClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliClass.Repository
{
    public class SortedWasteRepository
    {
        private readonly string _connectionstring;

        public SortedWasteRepository (string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public SortedWaste GetSortedbyID(int id)
        {
            SortedWaste sortedWaste = null;

            using (var connection = new NpgsqlConnection(_connectionstring))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM sortedwaste WHERE wasteid = @wasteid", connection))
                {
                    command.Parameters.AddWithValue("@wasteid", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sortedWaste = new SortedWaste 
                            {
                               Wasteid = reader.GetInt32(reader.GetOrdinal("wasteid")),
                               WasteType = (SortedType)Enum.Parse(typeof(SortedType), reader.GetString(reader.GetOrdinal("wastedtype"))),
                               Quantity = reader.GetDouble(reader.GetOrdinal("quantity"))
                            };
                        }
                    }
                }
            }

            return sortedWaste;
        }
        public void CreateSortedWaste(SortedWaste sortedWaste)
        {
            using (var connection = new NpgsqlConnection(_connectionstring))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(
                    "INSERT INTO sortedwaste (wasteid, wastedtype, quantity) VALUES (@wasteid, @wastedtype::wasted_type, @quantity)", connection))
                {
                    command.Parameters.AddWithValue("@wasteid", sortedWaste.Wasteid);
                    command.Parameters.AddWithValue("@wastedtype", sortedWaste.WasteType.ToString());
                    command.Parameters.AddWithValue("@quantity", sortedWaste.Quantity);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateSortedWaste(SortedWaste sortedwaste)
        {
            using (var connection = new NpgsqlConnection(_connectionstring))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(
                    "UPDATE sortedwaste SET wastedtype = @wastetype::wasted_type, quantity = @quantity WHERE wasteid = @wasteid", connection)
                    )
                {
                    command.Parameters.AddWithValue("@wasteid", sortedwaste.Wasteid);
                    command.Parameters.AddWithValue("@wastetype", sortedwaste.WasteType.ToString());
                    command.Parameters.AddWithValue("@quantity", sortedwaste.Quantity);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSortedWaste(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionstring))
            {
                connection.Open();

                using (var command = new NpgsqlCommand
                    (
                    "DELETE FROM sortedwaste WHERE wasteid = @wasteid", connection
                    ))
                {
                    command.Parameters.AddWithValue("@wasteid", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

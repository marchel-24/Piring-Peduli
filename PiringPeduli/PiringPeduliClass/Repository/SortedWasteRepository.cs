using Npgsql;
using PiringPeduliClass.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public SortedWaste? GetSortedByType(string type)
        {
            SortedWaste? sortedWaste = null;

            using (var connection = new NpgsqlConnection(_connectionstring))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM sortedwaste WHERE wastetype = @wastetype", connection))
                {
                    command.Parameters.AddWithValue("@wastetype", type);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sortedWaste = new SortedWaste 
                            {
                               Wasteid = reader.GetInt32(reader.GetOrdinal("wasteid")),
                               WasteType = reader.GetString(reader.GetOrdinal("wastetype")),
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
                    "INSERT INTO sortedwaste (wasteid, wastetype) VALUES (DEFAULT, @wastetype)", connection))
                {
                    //command.Parameters.AddWithValue("@wasteid", sortedWaste.Wasteid);
                    command.Parameters.AddWithValue("@wastetype", sortedWaste.WasteType);
                    //command.Parameters.AddWithValue("@quantity", sortedWaste.Quantity);

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
                    "UPDATE sortedwaste SET wastedtype = @wastetype::wasted_type WHERE wasteid = @wasteid", connection)
                    )
                {
                    command.Parameters.AddWithValue("@wasteid", sortedwaste.Wasteid);
                    command.Parameters.AddWithValue("@wastetype", sortedwaste.WasteType.ToString());
                    //command.Parameters.AddWithValue("@quantity", sortedwaste.Quantity);

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

        public List<string> GetAllWasteType()
        {
            var wasteTypes = new List<string>();

            using (var connection = new NpgsqlConnection(_connectionstring))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM sortedwaste", connection))
                {

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            wasteTypes.Add(reader.GetString(reader.GetOrdinal("wastetype")))
                            ;
                        }
                    }
                }
            }
            Debug.WriteLine(wasteTypes.Count);
            return wasteTypes;
        }
    }
}

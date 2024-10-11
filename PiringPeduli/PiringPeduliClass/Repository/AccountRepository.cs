using Npgsql;
using PiringPeduliClass.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliClass.Repository
{
    public class AccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Account GetAccountById(int id)
        {
            Account account = null;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM account WHERE accountid = @accountid", connection))
                {
                    command.Parameters.AddWithValue("@accountid", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            account = new Account
                            {
                                AccountId = reader.GetInt32(reader.GetOrdinal("accountid")),
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                Type = (AccountType)Enum.Parse(typeof(AccountType), reader.GetString(reader.GetOrdinal("type")))
                            };
                        }
                    }
                }
            }

            return account;
        }
    }
}

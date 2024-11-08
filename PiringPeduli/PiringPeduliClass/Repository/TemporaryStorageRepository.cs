using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliClass.Repository
{
    public class TemporaryStorageRepository
    {
        private readonly string _connectionString;

        public TemporaryStorageRepository (string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}

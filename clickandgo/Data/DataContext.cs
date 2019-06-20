using clickandgo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clickandgo.Data
{
    public class DataContext
    {
        private readonly IMongoDatabase _database = null;

        public DataContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }
        }

        public IMongoCollection<Users> Users
        {
            get {
                return _database.GetCollection<Users>("users");
            }
        }

        public IMongoCollection<Vehicle> Vehicle
        {
            get
            {
                return _database.GetCollection<Vehicle>("vehicle");
            }
        }

        public IMongoCollection<Driver> Driver
        {
            get
            {
                return _database.GetCollection<Driver>("driver");
            }
        }
    }
}

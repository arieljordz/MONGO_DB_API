using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MONGO_DB_API.Models.Entities;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MONGO_DB_API.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        // Expose a method to get a collection
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}

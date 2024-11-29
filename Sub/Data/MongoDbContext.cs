using MongoDB.Driver;
using Sub.Data.Models;

namespace Sub.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoClient mongoClient, string databaseName)
        {
            _database = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<Tarefa> Tarefas => _database.GetCollection<Tarefa>("tarefas");
    }
}

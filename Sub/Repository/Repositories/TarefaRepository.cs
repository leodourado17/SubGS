using MongoDB.Driver;
using Sub.Data;
using Sub.Data.Models;
using Sub.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sub.Repository.Repositories
{
    public class TarefaRepository : IRepository<Tarefa>
    {
        private readonly IMongoCollection<Tarefa> _collection;

        public TarefaRepository(MongoDbContext context)
        {
            _collection = context.Tarefas;
        }

        public async Task<IEnumerable<Tarefa>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Tarefa> GetByIdAsync(string id)
        {
            return await _collection.Find(t => t.Guid.ToString() == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Tarefa entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(string id, Tarefa entity)
        {
            entity.Guid = Guid.Parse(id);
            var filter = Builders<Tarefa>.Filter.Eq(t => t.Guid, entity.Guid);

            var updateOptions = new ReplaceOptions { IsUpsert = false };
            await _collection.ReplaceOneAsync(filter, entity, updateOptions);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(t => t.Guid.ToString() == id);
        }
    }
}

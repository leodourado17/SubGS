using Sub.Data.Models;
using Sub.Repository;

namespace Sub.Service
{
    public class TarefaService
    {
        private readonly IRepository<Tarefa> _repository;

        public TarefaService(IRepository<Tarefa> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Tarefa>> ObterTodasTarefasAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Tarefa> ObterTarefaPorIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CriarTarefaAsync(Tarefa tarefa)
        {
            await _repository.AddAsync(tarefa);
        }

        public async Task AtualizarTarefaAsync(string id, Tarefa tarefa)
        {
            if (tarefa.Guid.ToString() != id)
            {
                tarefa.Guid = Guid.Parse(id);
            }
            await _repository.UpdateAsync(id, tarefa);
        }


        public async Task DeletarTarefaAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

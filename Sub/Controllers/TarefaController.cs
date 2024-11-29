using Microsoft.AspNetCore.Mvc;
using Sub.Data.Models;
using Sub.Service;

namespace Sub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaService _service;

        public TarefaController(TarefaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtém todas as tarefas.
        /// </summary>
        /// <returns>Uma lista de todas as tarefas.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tarefas = await _service.ObterTodasTarefasAsync();
            return Ok(tarefas);
        }

        /// <summary>
        /// Obtém uma tarefa específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da tarefa.</param>
        /// <returns>A tarefa solicitada ou um erro se não encontrada.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var tarefa = await _service.ObterTarefaPorIdAsync(id);
            if (tarefa == null)
            {
                return NotFound(new { Message = "Tarefa não encontrada." });
            }
            return Ok(tarefa);
        }

        /// <summary>
        /// Cria uma nova tarefa.
        /// </summary>
        /// <param name="tarefa">O objeto tarefa a ser criada.</param>
        /// <returns>O status da criação da tarefa.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tarefa tarefa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.CriarTarefaAsync(tarefa);
            return CreatedAtAction(nameof(GetById), new { id = tarefa.Guid }, tarefa);
        }

        /// <summary>
        /// Atualiza uma tarefa existente.
        /// </summary>
        /// <param name="id">O ID da tarefa a ser atualizada.</param>
        /// <param name="tarefa">Os dados da tarefa para atualização.</param>
        /// <returns>O status da atualização da tarefa.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Tarefa tarefa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (tarefa.Guid.ToString() != id)
            {
                return BadRequest(new { Message = "O ID no corpo da requisição não corresponde ao ID da URL." });
            }

            var existingTarefa = await _service.ObterTarefaPorIdAsync(id);
            if (existingTarefa == null)
            {
                return NotFound(new { Message = "Tarefa não encontrada." });
            }

            await _service.AtualizarTarefaAsync(id, tarefa);
            return NoContent();
        }

        /// <summary>
        /// Exclui uma tarefa.
        /// </summary>
        /// <param name="id">O ID da tarefa a ser excluída.</param>
        /// <returns>O status da exclusão da tarefa.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingTarefa = await _service.ObterTarefaPorIdAsync(id);
            if (existingTarefa == null)
            {
                return NotFound(new { Message = "Tarefa não encontrada." });
            }

            await _service.DeletarTarefaAsync(id);
            return NoContent();
        }
    }
}

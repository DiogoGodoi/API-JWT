using CamadaController;
using CamadaModelo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_JWT.Controllers
{
	[Route("v1")]
	[ApiController]
	public class TarefasController : ControllerBase
	{

		private readonly CtrlTarefas tarefas;

		public TarefasController(CtrlTarefas tarefas)
		{
			this.tarefas = tarefas;
		}

		[HttpGet]
		[Authorize(Roles = "Usuario, Master")]
		[Route("tarefa/all")]
		public async Task<IActionResult> GetTarefas()
		{
			var jobs = await tarefas.GetTarefas();
			return Ok(jobs);
		}

		[HttpGet]
		[Authorize(Roles = "Usuario, Master")]
		[Route("tarefa/{id}")]
		public async Task<IActionResult> GetTarefasById(int id)
		{
			var tarefa = await tarefas.GetTarefasById(id);
			if (tarefa == null)
			{
				return NotFound("Tarefa não encontrada");
			}
			else
			{
				return Ok(tarefa);
			}
		}

		[HttpPost]
		[Authorize(Roles = "Master")]
		[Route("tarefa/create")]
		public async Task<IActionResult> CreateTarefas([FromBody] MdlTarefas pmtTarefas)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Houve um erro no model");
			}

			var retorno = await tarefas.CreateTarefa(pmtTarefas);

			if (retorno == true)
			{
				return Created($"v1/tarefa/{pmtTarefas.id}", pmtTarefas);
			}
			else
			{
				return BadRequest("Houve um erro na inserção");
			}
		}

		[HttpPut]
		[Authorize(Roles = "Master")]
		[Route("tarefa/update/{id}")]
		public async Task<IActionResult> UpdateTarefa([FromRoute]int id, [FromBody]MdlTarefas pmtTarefas)
		{
			var tarefa = await tarefas.UpdateTarefa(id, pmtTarefas);
			if (tarefa == false)
			{
				return NotFound("Tarefa não encontrada");
			}
			else
			{
				return Ok("Tarefa atualizada");
			}
		}

		[HttpDelete]
		[Authorize(Roles = "Master")]
		[Route("tarefa/delete/{id}")]
		public async Task<IActionResult> RemoveTarefa([FromRoute] int id)
		{
			var tarefa = await tarefas.RemoveTarefa(id);
			if (tarefa == false)
			{
				return NotFound("Tarefa não encontrada");
			}
			else
			{
				return Ok("Tarefa removida");
			}
		}


	}
}

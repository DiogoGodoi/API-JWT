using CamadaModelo;
using Microsoft.EntityFrameworkCore;

namespace CamadaAcessoDados
{
	public class RepositoryTarefas : IRepositoryTarefas
	{
		private readonly MeuContexto _context;
		public RepositoryTarefas(MeuContexto _context)
		{
			this._context = _context;
		}
		public async Task<bool> CreateTarefa(MdlTarefas tarefa)
		{
			try
			{
				await _context.Tarefas.AddAsync(tarefa);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}

		}
		public async Task<IEnumerable<MdlTarefas>> GetTarefas()
		{
			return await _context.Tarefas.ToListAsync();
		}
		public async Task<MdlTarefas> GetTarefasByID(int id)
		{
			var tarefa = await _context.Tarefas.FirstOrDefaultAsync(i => i.id == id);

			if (tarefa == null)
			{
				return new MdlTarefas();
			}
			else
			{
				return tarefa;
			}

		}
		public async Task<bool> UpdateTarefa(int id, MdlTarefas pmtTarefa)
		{
			var tarefa = await _context.Tarefas.FirstOrDefaultAsync(i => i.id == id);
			if (tarefa == null)
			{
				return false;
			}
			else
			{
				tarefa.nome = pmtTarefa.nome;
				tarefa.date = pmtTarefa.date;
				tarefa.completa = pmtTarefa.completa;

				await _context.SaveChangesAsync();
				return true;
			}
		}
		public async Task<bool> RemoveTarefa(int id)
		{
			try
			{
				var tarefa = await _context.Tarefas.FirstOrDefaultAsync(i => i.id == id);

				if (tarefa == null)
				{
					return false;
				}
				else
				{
					_context.Tarefas.Remove(tarefa);
					await _context.SaveChangesAsync();
					return true;
				}
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}

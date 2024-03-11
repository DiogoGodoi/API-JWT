using CamadaModelo;
using Microsoft.EntityFrameworkCore;

namespace CamadaAcessoDados
{
	public class RepositoryTarefas : IRepositoryTarefas
	{
		private readonly Dao dao;
		public RepositoryTarefas(Dao dao)
		{
			this.dao = dao;
		}
		public async Task<bool> CreateTarefa(MdlTarefas tarefa)
		{
			try
			{
				await dao.Tarefas.AddAsync(tarefa);
				await dao.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}

		}
		public async Task<IEnumerable<MdlTarefas>> GetTarefas()
		{
			return await dao.Tarefas.ToListAsync();
		}
		public async Task<MdlTarefas> GetTarefasByID(int id)
		{
			var tarefa = await dao.Tarefas.FirstOrDefaultAsync(i => i.id == id);

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
			var tarefa = await dao.Tarefas.FirstOrDefaultAsync(i => i.id == id);
			if (tarefa == null)
			{
				return false;
			}
			else
			{
				tarefa.nome = pmtTarefa.nome;
				tarefa.date = pmtTarefa.date;
				tarefa.completa = pmtTarefa.completa;

				await dao.SaveChangesAsync();
				return true;
			}
		}
		public async Task<bool> RemoveTarefa(int id)
		{
			try
			{
				var tarefa = await dao.Tarefas.FirstOrDefaultAsync(i => i.id == id);

				if (tarefa == null)
				{
					return false;
				}
				else
				{
					dao.Tarefas.Remove(tarefa);
					await dao.SaveChangesAsync();
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

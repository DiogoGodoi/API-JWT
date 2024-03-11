using CamadaAcessoDados;
using CamadaModelo;

namespace CamadaController
{
	public class CtrlTarefas
	{
		private readonly IRepositoryTarefas repositoryTarefas;
		public CtrlTarefas(IRepositoryTarefas repositoryTarefas)
		{
			this.repositoryTarefas = repositoryTarefas;
		}
		public async Task<IEnumerable<MdlTarefas>> GetTarefas()
		{
			return await repositoryTarefas.GetTarefas();
		}
		public async Task<MdlTarefas> GetTarefasById(int id)
		{
			return await repositoryTarefas.GetTarefasByID(id);
		}
		public async Task<bool> CreateTarefa(MdlTarefas tarefa)
		{
			try
			{
				var retorno = await repositoryTarefas.CreateTarefa(tarefa);
				if (retorno == true)
				{
					return true;
				}
				else
				{
					return false;
				}

			}
			catch (Exception ex)
			{
				return false;
			}

		}
		public async Task<bool> UpdateTarefa(int id, MdlTarefas tarefa)
		{
			try
			{
				var retorno = await repositoryTarefas.UpdateTarefa(id, tarefa);
				if (retorno == true)
				{
					return true;
				}
				else
				{
					return false;
				}

			}
			catch (Exception ex)
			{
				return false;
			}

		}
		public async Task<bool> RemoveTarefa(int id)
		{
			try
			{
				var retorno = await repositoryTarefas.RemoveTarefa(id);

				if (retorno == true)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				return false;
			}

		}
	}
}

using CamadaModelo;

namespace CamadaAcessoDados
{
	public interface IRepositoryTarefas
	{
		Task<bool> CreateTarefa(MdlTarefas tarefa);
		Task<IEnumerable<MdlTarefas>> GetTarefas();
		Task<MdlTarefas> GetTarefasByID(int id);
		Task<bool> UpdateTarefa(int id, MdlTarefas pmtTarefa);
		Task<bool> RemoveTarefa(int id);
	}
}

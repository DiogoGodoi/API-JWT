using System.ComponentModel.DataAnnotations;

namespace CamadaModelo
{
	public class MdlTarefas
	{
		public int id { get; set; }
		[Required]
		public string nome { get; set; } = "";
		[Required]
		public DateTime date { get; set; } = DateTime.Now;
		public bool completa { get; set; }
	}
}

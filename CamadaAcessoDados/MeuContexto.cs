﻿using CamadaModelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CamadaAcessoDados
{
	public class MeuContexto: DbContext
	{
		public MeuContexto(DbContextOptions<MeuContexto> options):base(options) { }

		public DbSet<MdlTarefas> Tarefas {get; set;}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json").Build();

			var connectionString = configuration.GetConnectionString("str");

			optionsBuilder.UseSqlServer(connectionString, options => options.MigrationsAssembly("API JWT"));
		}
	}
}

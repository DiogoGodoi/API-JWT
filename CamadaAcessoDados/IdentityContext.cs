using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CamadaAcessoDados
{
	public class IdentityContext: IdentityDbContext<IdentityUser>
	{
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json").Build();

			var connectionString = configuration.GetConnectionString("strIdentity");

			optionsBuilder.UseSqlServer(connectionString, options => options.MigrationsAssembly("API JWT"));

		}

	}
}

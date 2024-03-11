using API_JWT.Service;
using CamadaAcessoDados;
using CamadaController;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.

var key = Encoding.ASCII.GetBytes(Key.key);

builder.Services.AddControllers();
builder.Services.AddDbContext<Dao>();
builder.Services.AddDbContext<IdentityContext>();
builder.Services.AddScoped<IRepositoryTarefas, RepositoryTarefas>();
builder.Services.AddScoped<CtrlTarefas>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
	options.SignIn.RequireConfirmedEmail = false;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = 3;
	options.Password.RequiredUniqueChars = 1;

})
	.AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.RequireHttpsMetadata = false;
		options.SaveToken = true;
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			IssuerSigningKey = new SymmetricSecurityKey(key),
			ValidateAudience = false,
			ValidateIssuerSigningKey = false
		};
	});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var serviceProvider = scope.ServiceProvider;
	var userManager = serviceProvider.GetRequiredService<IUserRoleService>();
	userManager.CreateRole();
	userManager.CreateUser();
}

// Configurar o pipeline de solicitação HTTP.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


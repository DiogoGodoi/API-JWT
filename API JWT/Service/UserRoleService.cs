using Microsoft.AspNetCore.Identity;

namespace API_JWT.Service
{
	public class UserRoleService : IUserRoleService
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserRoleService(UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> roleManager)
		{
			this._userManager = _userManager;
			_roleManager = roleManager;
		}

		public void CreateRole()
		{
			if (!_roleManager.RoleExistsAsync("Master").Result)
			{
				IdentityRole role = new IdentityRole();
				role.Name = "Master";
				role.NormalizedName = "MASTER";
				IdentityResult result = _roleManager.CreateAsync(role).Result;
			}
			if (!_roleManager.RoleExistsAsync("Admin").Result)
			{
				IdentityRole role = new IdentityRole();
				role.Name = "Admin";
				role.NormalizedName = "ADMIN";
				IdentityResult result = _roleManager.CreateAsync(role).Result;
			}
			if (!_roleManager.RoleExistsAsync("Usuario").Result)
			{
				IdentityRole role = new IdentityRole();
				role.Name = "Usuario";
				role.NormalizedName = "USUARIO";
				IdentityResult result = _roleManager.CreateAsync(role).Result;
			}
		}

		public void CreateUser()
		{
			IdentityResult resultado = new IdentityResult();

			if (_userManager.FindByEmailAsync("admin@localhost.com").Result == null)
			{
				IdentityUser admin = new IdentityUser();

				admin.UserName = "Admin";
				admin.NormalizedUserName = "ADMIN";
				admin.Email = "admin@localhost.com";
				admin.NormalizedEmail = "ADMIN@LOCALHOST.COM";
				admin.EmailConfirmed = true;
				admin.LockoutEnabled = false;
				admin.SecurityStamp = Guid.NewGuid().ToString();

				resultado = _userManager.CreateAsync(admin, "Admin1994").Result;

				if (resultado.Succeeded)
				{
					_userManager.AddToRoleAsync(admin, "Master").Wait();
				}

			}
			else if (_userManager.FindByEmailAsync("usuario@localhost.com").Result == null)
			{
				IdentityUser usuario = new IdentityUser();

				usuario.UserName = "Usuario";
				usuario.NormalizedUserName = "USUARIO";
				usuario.Email = "usuario@localhost.com";
				usuario.NormalizedEmail = "USUARIO@LOCALHOST.COM";
				usuario.EmailConfirmed = true;
				usuario.LockoutEnabled = false;
				usuario.SecurityStamp = Guid.NewGuid().ToString();

				resultado = _userManager.CreateAsync(usuario, "Usuario1994").Result;

				if (resultado.Succeeded)
				{
					_userManager.AddToRoleAsync(usuario, "Usuario").Wait();
				}
			}
		}
	}
}
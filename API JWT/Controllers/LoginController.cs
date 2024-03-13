using API_JWT.Service;
using CamadaModelo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_JWT.Controllers
{
	[Route("v1/login")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signManager;
		private readonly ITokenService _tokenService;
		public LoginController(UserManager<IdentityUser> _userManager,
			SignInManager<IdentityUser> _signManager,
			ITokenService _tokenService)
		{
			this._userManager = _userManager;
			this._signManager = _signManager;
			this._tokenService = _tokenService;
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] MdlUsuario usuario)
		{
			try
			{
			
				var userFind = await _userManager.FindByEmailAsync(usuario.email);

				if (userFind != null)
				{
					var resposta = await _signManager.PasswordSignInAsync(userFind, usuario.password, false, false);

					if (resposta.Succeeded)
					{
						var token = _tokenService.GenerateToken(userFind);
						return Ok(token);
					}
					else
					{
						return StatusCode(401);
					}
				}
				else
				{
					return NotFound("Usuário não localizado");
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

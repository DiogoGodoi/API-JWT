using CamadaModelo;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_JWT.Service
{
	public class TokenService: ITokenService
	{
		public string GenerateToken(IdentityUser usuario)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var chave = Encoding.ASCII.GetBytes(Key.key);
			var chaveSimetrica = new SymmetricSecurityKey(chave);
			var algoritimo = SecurityAlgorithms.HmacSha256;
			var credenciais = new SigningCredentials(chaveSimetrica, algoritimo);
			var claims = new ClaimsIdentity(new Claim[]
			{
				new Claim(ClaimTypes.Name, usuario.Id),
				new Claim(ClaimTypes.Email, usuario.Email),
			});

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Expires = DateTime.UtcNow.AddHours(2),
				SigningCredentials = credenciais,
				Subject = claims
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);	
		}
	}
}

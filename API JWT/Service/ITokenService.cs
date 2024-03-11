using Microsoft.AspNetCore.Identity;

namespace API_JWT.Service
{
	public interface ITokenService
	{
		string GenerateToken(IdentityUser usuario);
	}
}

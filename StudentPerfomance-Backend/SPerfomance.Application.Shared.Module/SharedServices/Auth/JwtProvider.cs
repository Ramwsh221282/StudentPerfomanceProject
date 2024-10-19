using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Shared.Module.SharedServices.Auth;

public class JwtProvider
{
	public const string _secret = "a2c744df-4775-410c-af5a-975a377dab1b";

	public string GenerateToken(User user)
	{
		Claim[] claims = [new("userId", user.Id.ToString())];

		SigningCredentials credentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret)),
			SecurityAlgorithms.HmacSha256
		);

		JwtSecurityToken token = new JwtSecurityToken(
			claims: claims,
			signingCredentials: credentials,
			expires: DateTime.UtcNow.AddHours(1)
		);

		var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
		return tokenValue;
	}

	public JwtVerificationObject VerifyToken(string token) => new JwtVerificationObject(token);
}

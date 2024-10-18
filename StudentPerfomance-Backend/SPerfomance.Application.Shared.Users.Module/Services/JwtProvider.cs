using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Shared.Users.Module.Services;

internal sealed class JwtProvider
{
	public string GenerateToken(User user)
	{
		Claim[] claims = [new("userId", user.Id.ToString()), new("role", user.Role.Value)];

		SigningCredentials credentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a2c744df-4775-410c-af5a-975a377dab1b")),
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
}

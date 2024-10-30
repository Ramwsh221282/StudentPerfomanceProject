using SPerfomance.Domain.Models.Users;

namespace SPerfomance.Application.Services.Authentication.Abstractions;

public interface IJwtTokenService
{
	public string GenerateToken(User user);
}

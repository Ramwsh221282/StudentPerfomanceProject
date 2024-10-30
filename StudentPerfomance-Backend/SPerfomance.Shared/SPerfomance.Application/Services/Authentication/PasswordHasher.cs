using SPerfomance.Application.Services.Authentication.Abstractions;

namespace SPerfomance.Application.Services.Authentication;

public class PasswordHasher : IPasswordHasher
{
	public string Hash(string password) =>
		BCrypt.Net.BCrypt.EnhancedHashPassword(password);

	public bool Verify(string password, string hashedPassowrd) =>
		BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassowrd);
}

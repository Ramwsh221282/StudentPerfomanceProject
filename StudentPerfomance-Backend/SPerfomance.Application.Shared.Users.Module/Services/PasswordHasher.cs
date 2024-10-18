namespace SPerfomance.Application.Shared.Users.Module.Services;

internal sealed class PasswordHasher
{
	public string Generate(string password) =>
		 BCrypt.Net.BCrypt.EnhancedHashPassword(password);

	public bool Verify(string password, string hashedPassword) =>
		BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}

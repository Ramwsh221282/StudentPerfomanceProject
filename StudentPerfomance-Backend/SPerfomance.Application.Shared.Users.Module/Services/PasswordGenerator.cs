namespace SPerfomance.Application.Shared.Users.Module.Services;

internal sealed class PasswordGenerator
{
	private const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";

	public string Generate()
	{
		int length = 10;
		var random = new Random();
		string password = new string(Enumerable.Repeat(characters, length).Select(s => s[random.Next(s.Length)]).ToArray());
		return password;
	}
}

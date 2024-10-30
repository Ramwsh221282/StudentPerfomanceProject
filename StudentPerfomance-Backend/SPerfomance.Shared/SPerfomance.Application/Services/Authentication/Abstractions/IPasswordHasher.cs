namespace SPerfomance.Application.Services.Authentication.Abstractions;

public interface IPasswordHasher
{
	public string Hash(string password);

	public bool Verify(string password, string hashedPassowrd);
}

namespace SPerfomance.Application.Shared.Users.Module.Repositories;

public sealed class UserRepositoryObject
{
	public string Email { get; private set; } = string.Empty;
	public string Name { get; private set; } = string.Empty;
	public string Surname { get; private set; } = string.Empty;
	public string Thirdname { get; private set; } = string.Empty;

	public UserRepositoryObject WithEmail(string email)
	{
		if (!string.IsNullOrWhiteSpace(email)) Email = email;
		return this;
	}

	public UserRepositoryObject WithName(string name)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		return this;
	}

	public UserRepositoryObject WithSurname(string surname)
	{
		if (!string.IsNullOrWhiteSpace(surname)) Surname = surname;
		return this;
	}

	public UserRepositoryObject WithThirdname(string thirdname)
	{
		if (!string.IsNullOrWhiteSpace(thirdname)) Thirdname = thirdname;
		return this;
	}
}

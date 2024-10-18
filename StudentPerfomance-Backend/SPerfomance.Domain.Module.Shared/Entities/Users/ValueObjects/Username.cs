using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects;

public class Username : ValueObject
{
	private Username()
	{
		Name = string.Empty;
		Surname = string.Empty;
		Thirdname = string.Empty;
	}

	public Username(string name, string surname, string? thirdname)
	{
		Name = name;
		Surname = surname;
		if (!string.IsNullOrWhiteSpace(thirdname)) Thirdname = thirdname;
	}

	public string Name { get; private set; }
	public string Surname { get; private set; }
	public string? Thirdname { get; private set; }

	public CSharpFunctionalExtensions.Result ChangeUserName(Username updated)
	{
		if (this == updated) return CSharpFunctionalExtensions.Result.Failure("Данные идентичны");
		Name = updated.Name;
		Surname = updated.Surname;
		Thirdname = updated.Thirdname;
		return CSharpFunctionalExtensions.Result.Success();
	}

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Name;
		yield return Surname;
		yield return string.IsNullOrWhiteSpace(Thirdname) ? string.Empty : Thirdname;
	}

	public static Username CreateDefault() => new Username();

	public static CSharpFunctionalExtensions.Result<Username> Create(string name, string surname, string? thirdname)
	{
		if (string.IsNullOrWhiteSpace(name))
			return CSharpFunctionalExtensions.Result.Failure<Username>(new NameEmptyError().ToString());
		if (string.IsNullOrWhiteSpace(surname))
			return CSharpFunctionalExtensions.Result.Failure<Username>(new SurnameEmptyError().ToString());
		return new Username(name, surname, string.IsNullOrWhiteSpace(thirdname) ? string.Empty : thirdname);
	}
}

using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.ValueObjects;

public sealed class Discipline : ValueObject
{
	public string Name { get; private set; }
	private Discipline() { Name = string.Empty; }
	private Discipline(string name) => Name = name;
	public static Discipline CreateDefault() => new Discipline();
	public static CSharpFunctionalExtensions.Result<Discipline> Create(string name)
	{

		CSharpFunctionalExtensions.Result<Discipline> result = new Discipline(name);
		return result.IsFailure ? CSharpFunctionalExtensions.Result.Failure<Discipline>(result.Error) : result;
	}

	public override string ToString() => Name;

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Name;
	}

	public void ChangeName(string name) => Name = name;
}

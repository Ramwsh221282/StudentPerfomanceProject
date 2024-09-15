using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.StudentGroup;

namespace StudentPerfomance.Domain.ValueObjects.StudentGroup;

public class GroupName : ValueObject
{
	private GroupName() { }

	private GroupName(string name) => Name = name;

	public string Name { get; }

	public static Result<GroupName> Create(string name)
	{
		Validator<GroupName> validator = new GroupNameValidator(name);
		if (!validator.Validate())
			return Result.Failure<GroupName>(validator.GetErrorText());
		return new GroupName(name);
	}

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Name;
	}
}

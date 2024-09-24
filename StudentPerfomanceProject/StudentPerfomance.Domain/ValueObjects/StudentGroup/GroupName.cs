using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.StudentGroup;

namespace StudentPerfomance.Domain.ValueObjects.StudentGroup;

public class GroupName : ValueObject
{
	private GroupName() { Name = string.Empty; }
	private GroupName(string name) => Name = name;
	public string Name { get; } = null!;
	public static Result<GroupName> Create(string name)
	{
		Validator<GroupName> validator = new GroupNameValidator(name);
		if (!validator.Validate())
			return Result.Failure<GroupName>(validator.GetErrorText());
		return new GroupName(name);
	}
	public static GroupName CreateDefault() => new GroupName();
	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Name;
	}
}

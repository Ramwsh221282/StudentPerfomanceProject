using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Validators;
using SPerfomance.Domain.Module.Shared.Extensions;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGroups.ValueObjects;

public sealed class GroupName : ValueObject
{
	private GroupName() { Name = string.Empty; }

	private GroupName(string name) => Name = name.FormatSpaces();

	public string Name { get; } = null!;

	public static GroupName CreateDefault() => new GroupName();

	public static CSharpFunctionalExtensions.Result<GroupName> Create(string name)
	{
		GroupName result = new GroupName(name);
		Validator<GroupName> validator = new GroupNameValidator(result);
		return validator.Validate() ? result : CSharpFunctionalExtensions.Result.Failure<GroupName>(validator.GetErrorText());
	}

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Name;
	}

	public override string ToString() => Name;
}

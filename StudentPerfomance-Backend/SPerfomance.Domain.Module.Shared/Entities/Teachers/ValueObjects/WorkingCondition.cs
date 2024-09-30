using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Validators;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects;

public class WorkingCondition : ValueObject
{
	public string Value { get; init; }
	protected WorkingCondition() => Value = string.Empty;
	protected WorkingCondition(string value) => Value = value;
	public static WorkingCondition CreateDefault() => new WorkingCondition();
	public static CSharpFunctionalExtensions.Result<WorkingCondition> Create(string value)
	{
		WorkingCondition condition = new WorkingCondition(value);
		Validator<WorkingCondition> validator = new WorkingConditionValidator(condition);
		return validator.Validate() ? condition : CSharpFunctionalExtensions.Result.Failure<WorkingCondition>(validator.GetErrorText());
	}
	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
	public override string ToString() => Value;
}

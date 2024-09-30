using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Students.Validators;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.ValueObjects;

public sealed class StudentState : ValueObject
{
	public static readonly StudentState Active = new StudentState("Активен");

	public static readonly StudentState NotActive = new StudentState("Неактивен");
	private StudentState()
	{
		State = string.Empty;
	}
	private StudentState(string state) => State = state;

	public string State { get; }
	public static StudentState CreateDefault() => new StudentState();
	public static CSharpFunctionalExtensions.Result<StudentState> Create(string state)
	{
		StudentState result = new StudentState(state);
		Validator<StudentState> validator = new StudentStateValidator(result);
		return validator.Validate() ? result : CSharpFunctionalExtensions.Result.Failure<StudentState>(validator.GetErrorText());
	}

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return State;
	}

	public override string ToString() => State;
}

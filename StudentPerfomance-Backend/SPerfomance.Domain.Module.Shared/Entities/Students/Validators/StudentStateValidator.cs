using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.Students.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Students.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.Validators;

internal sealed class StudentStateValidator(StudentState state) : Validator<StudentState>
{
	private readonly StudentState _state = state;
	private static readonly StudentState[] _states = { StudentState.Active, StudentState.NotActive };
	public override bool Validate()
	{
		if (string.IsNullOrWhiteSpace(_state.State))
			error.AppendError(new StudentStateError());
		if (_states.Any(item => item.State == _state.State) == false)
			error.AppendError(new StudentStateError());
		return HasError;
	}
}

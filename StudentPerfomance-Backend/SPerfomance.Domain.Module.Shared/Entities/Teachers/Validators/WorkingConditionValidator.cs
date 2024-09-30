using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Validators;

internal sealed class WorkingConditionValidator(WorkingCondition condition) : Validator<WorkingCondition>
{
	private readonly WorkingCondition[] _conditions =
	[
		new FullTimeWorkingCondition(),
		new PartTimeWorkingCondition(),
		new RemotePartTimeWorkingCondition(),
	];
	private readonly WorkingCondition _condition = condition;
	public override bool Validate()
	{
		if (_condition == null)
			error.AppendError(new WorkingConditionNullError());
		if (_condition != null && string.IsNullOrWhiteSpace(_condition.Value))
			error.AppendError(new WorkingConditionNullError());
		if (_condition != null && !string.IsNullOrWhiteSpace(_condition.Value) &&
		 	_conditions.Any(c => c.Value == _condition.Value) == false)
			error.AppendError(new WorkingConditionTypeError());
		return HasError;
	}
}

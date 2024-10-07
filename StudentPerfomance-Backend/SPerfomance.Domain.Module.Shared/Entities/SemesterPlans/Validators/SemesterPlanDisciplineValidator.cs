using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Validators;

internal sealed class SemesterPlanDisciplineValidator(Discipline discipline) : Validator<Discipline>
{
	private int maxLength = 100;
	private readonly Discipline _discipline = discipline;
	private readonly Dictionary<Error, bool> _validations = [];
	public override bool Validate()
	{
		if (_discipline == null)
			_validations.Add(new SemesterPlanDisciplineNullError(), true);
		if (_discipline != null && string.IsNullOrWhiteSpace(_discipline.Name))
			_validations.Add(new SemesterPlanDisciplineNameEmptyError(), true);
		if (_discipline != null && !string.IsNullOrWhiteSpace(_discipline.Name) && _discipline.Name.Length > maxLength)
			_validations.Add(new SemesterPlanDisciplineNameExceessLengthError(maxLength), true);
		foreach (var validation in _validations)
		{
			error.AppendError(validation.Key);
		}
		return _validations.Count == 0;
	}
}

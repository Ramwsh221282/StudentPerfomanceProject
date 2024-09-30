using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.StudentGrades.Errors;
using SPerfomance.Domain.Module.Shared.Entities.StudentGrades.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGrades.Validators;

internal sealed class GradeValueValidator(GradeValue grade) : Validator<GradeValue>
{
	private readonly GradeValue[] _values =
	[
		GradeValue.NotAttestated,
		GradeValue.Bad,
		GradeValue.Satisfine,
		GradeValue.Good,
		GradeValue.VeryGood,
	];
	private readonly GradeValue _grade = grade;
	public override bool Validate()
	{
		if (_grade == null)
			error.AppendError(new GradeValueError());
		if (_grade != null && _values.Any(item => item.Value == _grade.Value) == false)
			error.AppendError(new GradeValueError());
		return HasError;
	}
}

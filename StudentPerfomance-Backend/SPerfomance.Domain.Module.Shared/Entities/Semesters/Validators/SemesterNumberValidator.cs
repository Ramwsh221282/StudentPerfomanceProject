using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.Semesters.Validators;

internal sealed class SemesterNumberValidator : Validator<SemesterNumber>
{
	private readonly SemesterNumber _number;
	private readonly SemesterNumber[] _semesterNumbers;

	public SemesterNumberValidator(SemesterNumber number)
	{
		_number = number;
		_semesterNumbers =
		[
			SemesterNumber.OneSemester,
			SemesterNumber.TwoSemester,
			SemesterNumber.ThreeSemester,
			SemesterNumber.FourSemester,
			SemesterNumber.FiveSemester,
			SemesterNumber.SixSemester,
			SemesterNumber.SevenSemenster,
			SemesterNumber.EightSemester,
			SemesterNumber.NineSemester,
			SemesterNumber.TenSemester,
			SemesterNumber.ElevenSemester,
		];
	}

	public override bool Validate()
	{
		if (_number == null) error.AppendError(new SemesterNumberError());
		if (_number != null && _semesterNumbers.Any(n => n.Value == _number.Value) == false)
			error.AppendError(new SemesterNumberTypeError());
		return HasError;
	}
}

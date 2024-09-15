using StudentPerfomance.Domain.ValueObjects.SemesterValueObjects;

namespace StudentPerfomance.Domain.Validators.Semesters;

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
            SemesterNumber.TenSemester
        ];
    }

    public override bool Validate()
    {
        if (_semesterNumbers.Any(n => n.Value == _number.Value) == false)
        {
            _errorBuilder.AppendLine("Некорректный номер семестра");
            return false;
        }
        return true;
    }
}

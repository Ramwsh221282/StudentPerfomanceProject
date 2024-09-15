using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Domain.Validators.Semesters;

internal class SemesterValidator : Validator<Semester>
{
    private readonly Semester _semester;

    public SemesterValidator(Semester semester) => _semester = semester;

    public override bool Validate()
    {
        if (!IsGroupValid())
        {
            _errorBuilder.AppendLine("Группа не валидна");
            return false;
        }
        return true;
    }

    private bool IsGroupValid() => _semester.Group != null;
}

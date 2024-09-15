using StudentPerfomance.Domain.ValueObjects.Student;

namespace StudentPerfomance.Domain.Validators.Student;

internal class StudentRecordBookValidator : Validator<StudentRecordBook>
{
    private readonly ulong _recordBook;

    public StudentRecordBookValidator(ulong recordBook) => _recordBook = recordBook;    

    public override bool Validate()
    {
        if (_recordBook <= 0)
        {
            _errorBuilder.AppendLine("Значение зачетной книжки не может быть отрицательным или 0");
            return false;
        }
        return true;
    }
}

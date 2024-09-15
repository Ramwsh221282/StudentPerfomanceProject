using StudentPerfomance.Domain.Validators.GradeValue;

namespace StudentPerfomance.Domain.Validators.StudentGrade;

internal class StudentGradeValidator : Validator<Entities.StudentGrade>
{
    private readonly Entities.StudentGrade _studentGrade;

    public StudentGradeValidator(Entities.StudentGrade studentGrade) =>
        _studentGrade = studentGrade;    

    public override bool Validate() =>
        ValidateGradeValue() && ValidateTeacher() && ValidateStudent() && ValidateDiscipline();    

    private bool ValidateGradeValue()
    {
        Validator<ValueObjects.StudentGrade.GradeValue> gradeValueValidator = new GradeValueValidator(_studentGrade.Value.Value);
        if (!gradeValueValidator.Validate())
        {
            _errorBuilder.AppendLine(gradeValueValidator.GetErrorText());
            return false;
        }
        return true;
    }

    private bool ValidateTeacher()
    {
        if (_studentGrade.Teacher == null)
        {
            _errorBuilder.AppendLine("Преподаватель не выбран");
            return false;
        }
        return true;
    }

    private bool ValidateStudent()
    {
        if (_studentGrade.Student == null)
        {
            _errorBuilder.AppendLine("Студент не выбран");
            return false;
        }
        return true;
    }

    private bool ValidateDiscipline()
    {
        if (_studentGrade.Discipline == null)
        {
            _errorBuilder.AppendLine("Дисциплина не выбрана");
            return false;
        }
        return true;
    }
}

using StudentPerfomance.Domain.ValueObjects.Teacher;

namespace StudentPerfomance.Domain.Validators.Teacher;

internal class TeacherValidator : Validator<Entities.Teacher>
{
    private readonly Validator<TeacherName> _nameValidator;

    public TeacherValidator(Entities.Teacher teacher)
    {
        _nameValidator = new TeacherNameValidator(teacher.Name.Name, teacher.Name.Surname, teacher.Name.Thirdname);
    }

    public override bool Validate()
    {
        if (!_nameValidator.Validate())
        {
            _errorBuilder.AppendLine(_nameValidator.GetErrorText());
            return false;
        }
        return true;
    }
}

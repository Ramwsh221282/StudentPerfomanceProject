using StudentPerfomance.Domain.Validators.StudentGroup;

namespace StudentPerfomance.Domain.Validators;

internal class StudentGroupValidator : Validator<Entities.StudentGroup>
{
    private readonly GroupNameValidator _nameValidator;

    public StudentGroupValidator(Entities.StudentGroup group) => _nameValidator = new GroupNameValidator(group.Name.Name);    

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

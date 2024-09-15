namespace StudentPerfomance.Domain.Validators.Student;

internal class StudentValidator : Validator<Entities.Student>
{    
    private readonly StudentNameValidator _nameValidator;
    private readonly StudentStateValidator _stateValidator;
    private readonly StudentRecordBookValidator _recordBookValidator;

    public StudentValidator(Entities.Student student)
    {
        _nameValidator = new StudentNameValidator(student.Name.Name, student.Name.Surname, student.Name.Thirdname);
        _stateValidator = new StudentStateValidator(student.State.State);
        _recordBookValidator = new StudentRecordBookValidator(student.Recordbook.Recordbook);
    }


    public override bool Validate()
    {
        if (!_nameValidator.Validate())
        {
            _errorBuilder.AppendLine(_nameValidator.GetErrorText());
            return false;
        }

        if (!_stateValidator.Validate())
        {
            _errorBuilder.AppendLine(_stateValidator.GetErrorText());
            return false;
        }

        if (!_recordBookValidator.Validate())
        {
            _errorBuilder.AppendLine(_recordBookValidator.GetErrorText());
            return false;
        }

        return true;
    }
}

using StudentPerfomance.Domain.ValueObjects;

namespace StudentPerfomance.Domain.Validators.Student;

internal class StudentStateValidator : Validator<StudentState>
{
    private readonly string _state;

    private static readonly StudentState[] _states = { StudentState.Active, StudentState.NotActive };

    public StudentStateValidator(string state) => _state = state;    

    public override bool Validate()
    {
        if (_states.Any(item => item.State == _state) == false)
        {
            _errorBuilder.AppendLine("Такого состояния не может быть у студента");
            return false;
        }
        return true;
    }
}

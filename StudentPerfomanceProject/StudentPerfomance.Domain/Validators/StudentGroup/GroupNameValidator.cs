using StudentPerfomance.Domain.ValueObjects.StudentGroup;

namespace StudentPerfomance.Domain.Validators.StudentGroup;

internal class GroupNameValidator : Validator<GroupName>
{
    private const int MAX_NAME_LENGTH = 15;

    private readonly string _name;

    public GroupNameValidator(string name) => _name = name;    

    public override bool Validate()
    {
        if (string.IsNullOrWhiteSpace(_name) || _name.Length > MAX_NAME_LENGTH)
        {
            _errorBuilder.AppendLine("Имя пустое или длина выше 15 символов");
            return false;
        }
        return true;
    }
}

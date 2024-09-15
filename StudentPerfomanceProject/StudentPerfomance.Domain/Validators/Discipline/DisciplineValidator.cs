namespace StudentPerfomance.Domain.Validators.Discipline;

internal class DisciplineValidator : Validator<Entities.Discipline>
{
    private const int MAX_NAME_LENGTH = 50;    
    private readonly string _name;

    public DisciplineValidator(string name) =>
        _name = name;

    public override bool Validate()
    {
        if (string.IsNullOrWhiteSpace(_name) || _name.Length > MAX_NAME_LENGTH)
        {
            _errorBuilder.AppendLine("Название дисциплины больше пустое или выше 50 символов");
            return false;
        }
        return true;        
    }
}

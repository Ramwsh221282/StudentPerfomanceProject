using StudentPerfomance.Domain.ValueObjects.EducationDirections;

namespace StudentPerfomance.Domain.Validators.EducationDIrections;

// Валидатор объекта значения Direction Name.
internal sealed class DirectionNameValidator(DirectionName name) : Validator<DirectionName>
{
	private const int MAX_NAME_LENGTH = 100;
	private readonly DirectionName _name = name;
	public override bool Validate()
	{
		if (string.IsNullOrWhiteSpace(_name.Name))
			_errorBuilder.AppendLine("Пустое название направления подготовки");
		if (!string.IsNullOrWhiteSpace(_name.Name) && _name.Name.Length > MAX_NAME_LENGTH)
			_errorBuilder.AppendLine("Название направления подготовки превышает ограничание в 100 символов");
		return _errorBuilder.Length == 0;
	}
}

using StudentPerfomance.Domain.ValueObjects.EducationPlans;

namespace StudentPerfomance.Domain.Validators.EducationPlans;

// Валидация года набора.
internal sealed class YearOfCreationValidator(YearOfCreation year) : Validator<YearOfCreation>
{
	private const uint MAX_VALUE_SIZE = 3000;
	private const uint MIN_VALUE_SIZE = 2000;
	private readonly YearOfCreation _year = year;
	public override bool Validate()
	{
		if (_year.Year < MIN_VALUE_SIZE) _errorBuilder.AppendLine("Недопустимое значение года направления");
		if (_year.Year > MAX_VALUE_SIZE) _errorBuilder.AppendLine("Недопустимое значение года направления");
		return _errorBuilder.Length == 0;
	}
}

using StudentPerfomance.Domain.ValueObjects.EducationDirections;

namespace StudentPerfomance.Domain.Validators.EducationDIrections;

internal sealed class DirectionCodeValidator(DirectionCode code) : Validator<DirectionCode>
{
	private readonly DirectionCode _code = code;
	private const int MAX_LENGTH_SIZE = 20;

	public override bool Validate()
	{
		if (string.IsNullOrWhiteSpace(_code.Code))
			_errorBuilder.AppendLine("Параметр Код направления был пустым");
		if (!string.IsNullOrEmpty(_code.Code) && _code.Code.Length > MAX_LENGTH_SIZE)
			_errorBuilder.AppendLine($"Недопустимая длина кода направления. Максимально {MAX_LENGTH_SIZE} символов");
		return _errorBuilder.Length == 0;
	}
}

using StudentPerfomance.Domain.ValueObjects.EducationDirections;

namespace StudentPerfomance.Domain.Validators.EducationDIrections;

internal sealed class DirectionTypeValidator(DirectionType type) : Validator<DirectionType>
{
	private readonly DirectionType _type = type;
	private readonly DirectionType[] _types = [new BachelorDirection(), new MagisterDirection()];
	public override bool Validate()
	{
		if (_types.Any(t => t.Type == _type.Type) == false)
			_errorBuilder.AppendLine("Недопустимый тип направления подготовки.").AppendLine("Допустимы только магистратура и бакалавриат.");
		return _errorBuilder.Length == 0;
	}
}

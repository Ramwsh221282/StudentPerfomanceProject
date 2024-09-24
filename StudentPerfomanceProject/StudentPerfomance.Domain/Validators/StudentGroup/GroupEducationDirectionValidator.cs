using StudentPerfomance.Domain.ValueObjects.StudentGroup;

namespace StudentPerfomance.Domain.Validators.StudentGroup;

internal sealed class GroupEducationDirectionValidator(GroupEducationDirection direction) : Validator<GroupEducationDirection>
{
	private readonly GroupEducationDirection _direction = direction;
	private readonly GroupEducationDirection[] _directions =
	[
		GroupBachelorDirection.Create().Value,
		GroupMagisterDirection.Create().Value];
	public override bool Validate()
	{
		if (string.IsNullOrWhiteSpace(_direction.Direction))
			_errorBuilder.AppendLine("Пустое название направления подготовки.");
		if (_directions.Any(value => value.Direction == _direction.Direction) == false)
			_errorBuilder.AppendLine("Направление подготовки принимает только следующие значения: 1) Бакалавриат 2) Магистратура");
		return _errorBuilder.Length == 0 ? true : false;
	}
}

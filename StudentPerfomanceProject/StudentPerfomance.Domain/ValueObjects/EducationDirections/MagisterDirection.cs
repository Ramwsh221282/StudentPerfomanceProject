namespace StudentPerfomance.Domain.ValueObjects.EducationDirections;

public sealed class MagisterDirection : DirectionType
{
	public MagisterDirection() => Type = DirectionTypeConstraints.MagisterType;
}

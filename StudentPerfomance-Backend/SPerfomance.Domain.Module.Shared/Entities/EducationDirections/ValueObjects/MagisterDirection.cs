namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;

public sealed class MagisterDirection : DirectionType
{
	public MagisterDirection() => Type = DirectionTypeConstraints.MagisterType;
}

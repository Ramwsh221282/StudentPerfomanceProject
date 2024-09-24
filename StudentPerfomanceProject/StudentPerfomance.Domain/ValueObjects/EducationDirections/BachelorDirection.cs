namespace StudentPerfomance.Domain.ValueObjects.EducationDirections;

public sealed class BachelorDirection : DirectionType
{
	public BachelorDirection() => Type = DirectionTypeConstraints.BachelorType;
}

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;

public sealed class BachelorDirection : DirectionType
{
	public BachelorDirection() => Type = DirectionTypeConstraints.BachelorType;
}

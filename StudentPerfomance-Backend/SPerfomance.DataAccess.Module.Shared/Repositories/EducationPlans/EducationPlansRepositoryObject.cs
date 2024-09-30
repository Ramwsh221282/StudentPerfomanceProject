using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;

public sealed class EducationPlansRepositoryObject
{
	public uint Year { get; private set; }
	public EducationDirectionsRepositoryObject Direction { get; private set; } = new EducationDirectionsRepositoryObject();
	public EducationPlansRepositoryObject WithYear(uint year)
	{
		Year = year;
		return this;
	}

	public EducationPlansRepositoryObject WithDirection(EducationDirectionsRepositoryObject direction)
	{
		Direction = direction;
		return this;
	}
}

namespace StudentPerfomance.DataAccess.Repositories.EducationPlans;

public sealed class EducationPlanRepositoryParameter
{
	public uint Year { get; private set; }

	public EducationPlanRepositoryParameter WithYear(uint year)
	{
		Year = year;
		return this;
	}
}

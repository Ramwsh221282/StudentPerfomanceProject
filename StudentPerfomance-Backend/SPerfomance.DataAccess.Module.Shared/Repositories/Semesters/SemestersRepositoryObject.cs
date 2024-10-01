using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;

public sealed class SemestersRepositoryObject
{
	public byte Number { get; private set; }
	public EducationPlansRepositoryObject Plan { get; private set; } = new EducationPlansRepositoryObject();
	public SemestersRepositoryObject WithNumber(byte number)
	{
		Number = number;
		return this;
	}
	public SemestersRepositoryObject WithPlan(EducationPlansRepositoryObject plan)
	{
		Plan = plan;
		return this;
	}
}

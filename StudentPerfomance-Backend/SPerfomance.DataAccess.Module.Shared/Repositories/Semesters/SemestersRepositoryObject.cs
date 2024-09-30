namespace SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;

public sealed class SemestersRepositoryObject
{
	public byte Number { get; private set; }
	public int PlanYear { get; private set; }
	public SemestersRepositoryObject WithNumber(byte number)
	{
		Number = number;
		return this;
	}

	public SemestersRepositoryObject WithPlanYear(int planYear)
	{
		PlanYear = planYear;
		return this;
	}
}

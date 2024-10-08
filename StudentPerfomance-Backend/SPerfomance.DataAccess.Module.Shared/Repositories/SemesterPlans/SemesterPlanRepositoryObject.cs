using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.SemesterPlans;

public sealed class SemesterPlanRepositoryObject
{
	public string DisciplineName { get; private set; } = string.Empty;
	public SemestersRepositoryObject Semester { get; private set; } = new SemestersRepositoryObject();

	public SemesterPlanRepositoryObject WithDisciplineName(string disciplineName)
	{
		if (!string.IsNullOrWhiteSpace(disciplineName)) DisciplineName = disciplineName;
		return this;
	}

	public SemesterPlanRepositoryObject WithSemester(SemestersRepositoryObject semester)
	{
		if (semester != null) Semester = semester;
		return this;
	}
}

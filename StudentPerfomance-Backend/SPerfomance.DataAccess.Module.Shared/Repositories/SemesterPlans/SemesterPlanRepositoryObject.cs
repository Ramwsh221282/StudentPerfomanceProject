using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.SemesterPlans;

public sealed class SemesterPlanRepositoryObject
{
	public string DisciplineName { get; private set; } = string.Empty;
	public SemestersRepositoryObject Semester { get; private set; } = new SemestersRepositoryObject();
	public TeacherRepositoryObject Teacher { get; private set; } = new TeacherRepositoryObject();

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

	public SemesterPlanRepositoryObject WithTeacher(TeacherRepositoryObject teacher)
	{
		if (teacher != null) Teacher = teacher;
		return this;
	}
}

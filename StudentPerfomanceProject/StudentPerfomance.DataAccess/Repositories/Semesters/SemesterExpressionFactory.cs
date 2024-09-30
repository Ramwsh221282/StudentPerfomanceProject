using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Semesters;

public static class SemesterExpressionFactory
{
	public static IRepositoryExpression<Semester> CreateHasSemesterExpression(SemestersRepositoryParameter semester, EducationPlanRepositoryParameter plan) =>
		new HasSemesterExpression(semester, plan);
	public static IRepositoryExpression<Semester> CreateFilter(SemestersRepositoryParameter semester, StudentGroupsRepositoryParameter group) =>
		new SemestersRepositoryFilterExpression(semester, group);
	public static IRepositoryExpression<Semester> CreateSemestersByGroupExpression(StudentGroupsRepositoryParameter group) =>
		new SemestersByGroupExpression(group);
}

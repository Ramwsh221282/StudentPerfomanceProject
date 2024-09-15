using StudentPerfomance.DataAccess.Repositories.Disciplines;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.SemesterPlans;

public static class SemesterPlansExpressionFactory
{
	public static IRepositoryExpression<SemesterPlan> CreateHasExpression(SemestersRepositoryParameter semester, DisciplineRepositoryParameter discipline) =>
		new HasSemesterPlanExpression(semester, discipline);

	public static IRepositoryExpression<SemesterPlan> CreateFilterExpression
	(
		StudentGroupsRepositoryParameter group,
		SemestersRepositoryParameter semester,
		DisciplineRepositoryParameter discipline
	) => new SemesterPlansRepositoryFilterExpression(group, semester, discipline);

	public static IRepositoryExpression<SemesterPlan> CreateByGroupAndSemesterExpression
	(
		SemestersRepositoryParameter semester,
		StudentGroupsRepositoryParameter group
	) => new SemesterPlansByGroupSemesterExpression(semester, group);
}

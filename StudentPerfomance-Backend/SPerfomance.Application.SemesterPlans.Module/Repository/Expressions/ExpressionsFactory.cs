using SPerfomance.DataAccess.Module.Shared.Repositories.SemesterPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;

internal static class ExpressionsFactory
{
	public static IRepositoryExpression<SemesterPlan> GetPlan(SemesterPlanRepositoryObject plan) =>
		new GetPlan(plan);

	public static IRepositoryExpression<Semester> GetSemester(SemesterPlanRepositoryObject plan) =>
		new GetSemester(plan);

	public static IRepositoryExpression<Teacher> GetTeacher(TeacherRepositoryObject teacher) =>
		new GetTeacher(teacher);

	public static IRepositoryExpression<SemesterPlan> Filter(SemesterPlanRepositoryObject plan) =>
		new Filter(plan);
}

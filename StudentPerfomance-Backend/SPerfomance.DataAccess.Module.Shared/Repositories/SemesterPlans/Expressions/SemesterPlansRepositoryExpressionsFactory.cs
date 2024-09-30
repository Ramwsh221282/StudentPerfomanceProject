using SPerfomance.DataAccess.Module.Shared.Repositories.Disciplines;
using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.SemesterPlans.Expressions;

public static class SemesterPlansRepositoryExpressionsFactory
{
	public static IRepositoryExpression<SemesterPlan> HasSemesterPlan(DisciplineRepositoryObject discipline, SemestersRepositoryObject semester) =>
		new HasSemesterPlan(discipline, semester);
}

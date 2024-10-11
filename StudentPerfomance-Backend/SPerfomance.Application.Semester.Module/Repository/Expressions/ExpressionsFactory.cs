using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;

namespace SPerfomance.Application.Semester.Module.Repository.Expressions;

internal static class ExpressionsFactory
{
	public static IRepositoryExpression<Domain.Module.Shared.Entities.Semesters.Semester> Filter(SemestersRepositoryObject semester) => new Filter(semester);
	public static IRepositoryExpression<Domain.Module.Shared.Entities.Semesters.Semester> EducationPlanSemesters(EducationPlansRepositoryObject plan) =>
		new EducationPlanSemestersExpression(plan);
}

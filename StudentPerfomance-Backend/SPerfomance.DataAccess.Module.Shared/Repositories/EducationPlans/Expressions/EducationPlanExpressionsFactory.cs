using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans.Expressions;

public static class EducationPlanExpressionsFactory
{
	public static IRepositoryExpression<EducationPlan> CreateFindPlan(EducationPlansRepositoryObject plan) =>
		 new FindEducationPlan(plan);
	public static IRepositoryExpression<EducationPlan> CreateFilter(EducationPlansRepositoryObject plan) =>
		new FilterEducationPlans(plan);
}

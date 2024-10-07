using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Repository.Expressions;

internal static class ExpressionsFactory
{
	public static IRepositoryExpression<EducationPlan> GetPlan(EducationPlansRepositoryObject plan) =>
		new GetPlan(plan);
	public static IRepositoryExpression<EducationDirection> GetDirection(EducationPlansRepositoryObject plan) =>
		new GetDirection(plan);
	public static IRepositoryExpression<EducationPlan> Filter(EducationPlansRepositoryObject plan) =>
		new Filter(plan);
}

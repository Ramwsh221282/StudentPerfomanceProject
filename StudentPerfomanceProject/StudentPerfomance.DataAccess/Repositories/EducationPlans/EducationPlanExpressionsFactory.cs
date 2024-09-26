using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.EducationPlans;

public static class EducationPlanExpressionsFactory
{
	public static IRepositoryExpression<EducationPlan> CreateFindPlan(EducationPlanRepositoryParameter plan, EducationDirectionRepositoryParameter direction) =>
		 new FindEducationPlanExpression(plan, direction);
	public static IRepositoryExpression<EducationPlan> CreateFilter(EducationPlanRepositoryParameter plan, EducationDirectionRepositoryParameter direction) =>
		new FilterEducationPlanExpression(plan, direction);
	public static IRepositoryExpression<EducationPlan> CreateFilterByDirection(EducationDirectionRepositoryParameter direction) =>
		new FilterEducationPlanByDirectionExpression(direction);
	public static IRepositoryExpression<EducationPlan> CreateFilterByYear(EducationPlanRepositoryParameter plan) =>
		new FilterEducationPlanExpressionByYearExpression(plan);
}

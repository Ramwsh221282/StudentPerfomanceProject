using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.EducationPlans;

public static class EducationPlanExpressionsFactory
{
	public static IRepositoryExpression<EducationPlan> CreateFindPlan(EducationPlanRepositoryParameter plan, EducationDirectionRepositoryParameter direction) =>
		 new FindEducationPlanExpression(plan, direction);
}

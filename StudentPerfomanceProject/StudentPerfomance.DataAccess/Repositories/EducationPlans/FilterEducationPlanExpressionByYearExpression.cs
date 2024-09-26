using System.Linq.Expressions;

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.EducationPlans;

public sealed class FilterEducationPlanExpressionByYearExpression(EducationPlanRepositoryParameter plan) : IRepositoryExpression<EducationPlan>
{
	private readonly EducationPlanRepositoryParameter _plan = plan;
	public Expression<Func<EducationPlan, bool>> Build() =>
		(EducationPlan entity) => entity.Year.Year == _plan.Year;

}

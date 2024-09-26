using System.Linq.Expressions;

using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.EducationPlans;

public sealed class FilterEducationPlanByDirectionExpression(EducationDirectionRepositoryParameter direction) : IRepositoryExpression<EducationPlan>
{
	private readonly EducationDirectionRepositoryParameter _direction = direction;
	public Expression<Func<EducationPlan, bool>> Build() =>
		(EducationPlan entity) =>
			entity.Direction.Name.Name.Contains(_direction.Name) ||
			entity.Direction.Code.Code.Contains(_direction.Code);
}

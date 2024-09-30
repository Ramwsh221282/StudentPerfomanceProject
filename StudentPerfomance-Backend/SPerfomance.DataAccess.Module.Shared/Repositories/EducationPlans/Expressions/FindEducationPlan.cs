using System.Linq.Expressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans.Expressions;

internal sealed class FindEducationPlan
(
	EducationPlansRepositoryObject plan
) : IRepositoryExpression<EducationPlan>
{
	private readonly EducationPlansRepositoryObject _plan = plan;
	public Expression<Func<EducationPlan, bool>> Build() =>
		(EducationPlan entity) =>
			entity.Year.Year == _plan.Year &&
			entity.Direction.Name.Name == _plan.Direction.Name &&
			entity.Direction.Code.Code == _plan.Direction.Code &&
			entity.Direction.Type.Type == _plan.Direction.Type;
}

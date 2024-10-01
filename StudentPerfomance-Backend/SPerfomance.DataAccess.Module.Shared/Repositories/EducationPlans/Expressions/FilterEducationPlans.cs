using System.Linq.Expressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans.Expressions;

internal sealed class FilterEducationPlans
(
	EducationPlansRepositoryObject plan
) : IRepositoryExpression<EducationPlan>
{
	private readonly EducationPlansRepositoryObject _plan = plan;
	public Expression<Func<EducationPlan, bool>> Build() =>
		(EducationPlan entity) =>
			entity.Year.Year == _plan.Year ||
			(!string.IsNullOrWhiteSpace(_plan.Direction.Name) && entity.Direction.Name.Name.Contains(_plan.Direction.Name)) ||
			(!string.IsNullOrWhiteSpace(_plan.Direction.Code) && entity.Direction.Code.Code.Contains(_plan.Direction.Code)) ||
			(!string.IsNullOrWhiteSpace(_plan.Direction.Type) && entity.Direction.Type.Type == _plan.Direction.Type);
}

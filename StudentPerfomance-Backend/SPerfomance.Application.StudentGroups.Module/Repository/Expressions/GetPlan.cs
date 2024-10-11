using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.StudentGroups.Module.Repository.Expressions;

internal sealed class GetPlan(EducationPlansRepositoryObject plan) : IRepositoryExpression<EducationPlan>
{
	private readonly EducationPlansRepositoryObject _plan = plan;

	public Expression<Func<EducationPlan, bool>> Build() =>
		(EducationPlan entity) =>
			entity.Year.Year == _plan.Year &&
			entity.Direction.Name.Name == _plan.Direction.Name &&
			entity.Direction.Code.Code == _plan.Direction.Code &&
			entity.Direction.Type.Type == _plan.Direction.Type;
}

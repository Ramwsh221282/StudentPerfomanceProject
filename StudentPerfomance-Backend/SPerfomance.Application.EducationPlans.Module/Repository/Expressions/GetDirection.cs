using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationPlans.Module.Repository.Expressions;

internal sealed class GetDirection(EducationPlansRepositoryObject plan) : IRepositoryExpression<EducationDirection>
{
	private readonly EducationPlansRepositoryObject _plan = plan;
	public Expression<Func<EducationDirection, bool>> Build() =>
		(EducationDirection entity) =>
			entity.Name.Name == _plan.Direction.Name &&
			entity.Code.Code == _plan.Direction.Code &&
			entity.Type.Type == _plan.Direction.Type;
}

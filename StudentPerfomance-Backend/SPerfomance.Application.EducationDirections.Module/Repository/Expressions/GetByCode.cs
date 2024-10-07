using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Repository.Expressions;

internal sealed class GetByCode(EducationDirectionsRepositoryObject direction) : IRepositoryExpression<EducationDirection>
{
	private readonly EducationDirectionsRepositoryObject _direction = direction;

	public Expression<Func<EducationDirection, bool>> Build() =>
		(EducationDirection entity) =>
			entity.Code.Code == _direction.Code;
}

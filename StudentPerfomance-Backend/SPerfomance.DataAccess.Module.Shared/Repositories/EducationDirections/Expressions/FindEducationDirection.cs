using System.Linq.Expressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections.Expressions;

internal sealed class FindEducationDirection(EducationDirectionsRepositoryObject direction) : IRepositoryExpression<EducationDirection>
{
	private readonly EducationDirectionsRepositoryObject _direction = direction;
	public Expression<Func<EducationDirection, bool>> Build() =>
		(EducationDirection entity) =>
			entity.Name.Name == _direction.Name &&
			entity.Code.Code == _direction.Code &&
			entity.Type.Type == _direction.Type;
}

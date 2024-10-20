using System.Linq.Expressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections.Expressions;

internal sealed class FilterEducationDirections(EducationDirectionsRepositoryObject direction) : IRepositoryExpression<EducationDirection>
{
	private readonly EducationDirectionsRepositoryObject _direction = direction;

	public Expression<Func<EducationDirection, bool>> Build() =>
		(EducationDirection entity) =>
			(!string.IsNullOrWhiteSpace(_direction.Name) && entity.Name.Name.Contains(_direction.Name)) ||
			(!string.IsNullOrWhiteSpace(_direction.Code) && entity.Code.Code.Contains(_direction.Code)) ||
			(!string.IsNullOrWhiteSpace(_direction.Type) && entity.Type.Type.Contains(_direction.Type));
}
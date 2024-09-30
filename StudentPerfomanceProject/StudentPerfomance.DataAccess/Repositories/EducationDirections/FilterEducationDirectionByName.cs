using System.Linq.Expressions;

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.EducationDirections;

public sealed class FilterEducationDirectionByName(EducationDirectionRepositoryParameter direction) : IRepositoryExpression<EducationDirection>
{
	private readonly EducationDirectionRepositoryParameter _direction = direction;

	public Expression<Func<EducationDirection, bool>> Build() =>
		(EducationDirection entity) =>
			entity.Name.Name.Contains(_direction.Name) ||
			entity.Name.Name.StartsWith(_direction.Name);
}

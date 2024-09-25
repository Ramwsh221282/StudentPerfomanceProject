using System.Linq.Expressions;

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.EducationDirections;

public sealed class FindEducationDirectionByNameExpression(EducationDirectionRepositoryParameter direction) : IRepositoryExpression<EducationDirection>
{
	private readonly EducationDirectionRepositoryParameter _direction = direction;

	public Expression<Func<EducationDirection, bool>> Build() =>
		(EducationDirection entity) => entity.Name.Name == _direction.Name;
}

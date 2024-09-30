using System.Linq.Expressions;

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.EducationDirections;

public sealed class FilterEducationDirectionByCode(EducationDirectionRepositoryParameter direction) : IRepositoryExpression<EducationDirection>
{
	private readonly EducationDirectionRepositoryParameter _direction = direction;
	public Expression<Func<EducationDirection, bool>> Build() =>
		(EducationDirection entity) =>
			entity.Code.Code.Contains(_direction.Code) ||
			entity.Code.Code.StartsWith(_direction.Code);
}

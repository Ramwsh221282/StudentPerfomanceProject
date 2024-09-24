using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.EducationDirections;

public static class EducationDirectionExpressionsFactory
{
	public static IRepositoryExpression<EducationDirection> FindDirection(EducationDirectionRepositoryParameter direction) =>
		new FindEducationDirectionExpression(direction);
}

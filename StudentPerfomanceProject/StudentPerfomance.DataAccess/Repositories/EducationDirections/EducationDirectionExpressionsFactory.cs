using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.EducationDirections;

public static class EducationDirectionExpressionsFactory
{
	public static IRepositoryExpression<EducationDirection> FindDirection(EducationDirectionRepositoryParameter direction) =>
		new FindEducationDirectionExpression(direction);
	public static IRepositoryExpression<EducationDirection> FindDirectionByName(EducationDirectionRepositoryParameter direction) =>
		new FindEducationDirectionByNameExpression(direction);
	public static IRepositoryExpression<EducationDirection> FindDirectionByCode(EducationDirectionRepositoryParameter direction) =>
		new FindEducationDirectionByCodeExpression(direction);
	public static IRepositoryExpression<EducationDirection> FilterExpression(EducationDirectionRepositoryParameter direction) =>
		new FilterEducationDirection(direction);
	public static IRepositoryExpression<EducationDirection> FilterByNameExpression(EducationDirectionRepositoryParameter direction) =>
		new FilterEducationDirectionByName(direction);
	public static IRepositoryExpression<EducationDirection> FilterByCodeExpression(EducationDirectionRepositoryParameter direction) =>
		new FilterEducationDirectionByCode(direction);
}

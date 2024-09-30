using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections.Expressions;

public static class EducationDirectionExpressionsFactory
{
	public static IRepositoryExpression<EducationDirection> FindDirection(EducationDirectionsRepositoryObject direction) =>
		new FindEducationDirection(direction);
	public static IRepositoryExpression<EducationDirection> FilterExpression(EducationDirectionsRepositoryObject direction) =>
		new FilterEducationDirections(direction);
	public static IRepositoryExpression<EducationDirection> FindDirectionByCode(EducationDirectionsRepositoryObject direction) =>
		new FindEducationDirectionByCode(direction);
}

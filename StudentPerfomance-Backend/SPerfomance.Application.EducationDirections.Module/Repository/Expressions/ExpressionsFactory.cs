using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Repository.Expressions;

internal static class ExpressionsFactory
{
	public static IRepositoryExpression<EducationDirection> GetByCode(EducationDirectionsRepositoryObject direction) =>
		new GetByCode(direction);
	public static IRepositoryExpression<EducationDirection> GetDirection(EducationDirectionsRepositoryObject direction) =>
		new GetDirection(direction);
	public static IRepositoryExpression<EducationDirection> Filter(EducationDirectionsRepositoryObject direction) =>
		new Filter(direction);
}

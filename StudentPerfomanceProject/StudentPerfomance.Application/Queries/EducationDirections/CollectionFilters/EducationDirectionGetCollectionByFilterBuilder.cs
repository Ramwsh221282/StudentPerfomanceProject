using StudentPerfomance.Application.Queries.EducationDirections.FilterConstraints;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationDirections.CollectionFilters;

public static class EducationDirectionGetCollectionByFilterBuilder
{
	public static IService<IReadOnlyCollection<EducationDirection>> Build
	(
		FilterConstraint constraint,
		IRepository<EducationDirection> repository,
		IRepositoryExpression<EducationDirection> expression
	)
	{
		return constraint.Constraint switch
		{
			FilterConstraint.General => new EducationDirectionGetCollectionByFilter(repository, expression),
			FilterConstraint.CodeOnly => new EducationDirectionGetCollectionByCodeFilterService(repository, expression),
			FilterConstraint.NameOnly => new EducationDirectionGetCollectionByNameFilterService(repository, expression),
			_ => new EducationDirectionGetCollectionByFilter(repository, expression),
		};
	}
}

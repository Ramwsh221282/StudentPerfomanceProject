using StudentPerfomance.Application.Queries.EducationDirections.FilterConstraints;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationDirections.CollectionPagedFilters;

public static class EducationDirectionGetPagedCollectionByFilterBuilder
{
	public static IService<IReadOnlyCollection<EducationDirection>> Build
	(
		FilterConstraint constraint,
		int page,
		int pageSize,
		IRepository<EducationDirection> repository,
		IRepositoryExpression<EducationDirection> expression
	)
	{
		return constraint.Constraint switch
		{
			FilterConstraint.General => new EducationDirectionGetPagedCollectionByFilterService(page, pageSize, repository, expression),
			FilterConstraint.CodeOnly => new EducationDirectionGetPagedCollectionByCodeFilterService(page, pageSize, repository, expression),
			FilterConstraint.NameOnly => new EducationDirectionGetPagedCollectionByNameFilterService(page, pageSize, repository, expression),
			_ => new EducationDirectionGetPagedCollectionByFilterService(page, pageSize, repository, expression),
		};
	}
}

using StudentPerfomance.Application.Queries.EducationPlans.FilterConstraints;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationPlans.CollectionPagedFilters;

public static class EducationPlanPagedCollectionFilterBuilder
{
	public static IService<IReadOnlyCollection<EducationPlan>> Build
	(
		int page,
		int pageSize,
		FilterConstraint constraint,
		IRepository<EducationPlan> repository,
		IRepositoryExpression<EducationPlan> expression
	)
	{
		return constraint.Constraint switch
		{
			FilterConstraint.General => new EducationPlanPagedCollectionFilter(page, pageSize, repository, expression),
			FilterConstraint.YearOnly => new EducationPlanPagedCollectionFilterByYear(page, pageSize, repository, expression),
			FilterConstraint.DirectionOnly => new EducationPlanPagedCollectionFilterByDirection(page, pageSize, repository, expression),
			_ => new EducationPlanPagedCollectionFilter(page, pageSize, repository, expression)
		};
	}
}

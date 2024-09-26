using StudentPerfomance.Application.Queries.EducationPlans.FilterConstraints;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationPlans.CollectionFilters;

public static class EducationPlanCollectionFilterBuilder
{
	public static IService<IReadOnlyCollection<EducationPlan>> Build
	(
		FilterConstraint constraint,
		IRepository<EducationPlan> repository,
		IRepositoryExpression<EducationPlan> expression
	)
	{
		return constraint.Constraint switch
		{
			FilterConstraint.General => new EducationPlanCollectionFilter(repository, expression),
			FilterConstraint.YearOnly => new EducationPlanCollectionFilterByYear(repository, expression),
			FilterConstraint.DirectionOnly => new EducationPlanCollectionFilterByDirection(repository, expression),
			_ => new EducationPlanCollectionFilter(repository, expression)
		};
	}
}

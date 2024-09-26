using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationPlans.CollectionPagedFilters;

public sealed class EducationPlanPagedCollectionFilterByDirection : EducationPlanPagedCollectionFilter, IService<IReadOnlyCollection<EducationPlan>>
{
	public EducationPlanPagedCollectionFilterByDirection(int page, int pageSize, IRepository<EducationPlan> repository, IRepositoryExpression<EducationPlan> expression)
		: base(page, pageSize, repository, expression) { }

	public new async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> DoOperation() => await Process();
}

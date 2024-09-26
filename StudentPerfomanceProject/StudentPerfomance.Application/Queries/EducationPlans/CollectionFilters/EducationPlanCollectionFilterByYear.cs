using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationPlans.CollectionFilters;

public class EducationPlanCollectionFilterByYear : EducationPlanCollectionFilter, IService<IReadOnlyCollection<EducationPlan>>
{
	public EducationPlanCollectionFilterByYear(IRepository<EducationPlan> repository, IRepositoryExpression<EducationPlan> expression)
	: base(repository, expression) { }
	public new async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> DoOperation() => await Process();
}

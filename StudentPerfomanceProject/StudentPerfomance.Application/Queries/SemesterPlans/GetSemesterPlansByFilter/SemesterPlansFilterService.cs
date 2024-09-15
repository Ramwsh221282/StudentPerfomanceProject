using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlansByFilter;

public sealed class SemesterPlansFilterService
(
	int page,
	int pageSize,
	IRepositoryExpression<SemesterPlan> expression,
	IRepository<SemesterPlan> repository
)
: IService<IReadOnlyCollection<SemesterPlan>>
{
	private readonly GetSemesterPlansByFilterQuery _query = new GetSemesterPlansByFilterQuery(page, pageSize, expression);
	private readonly GetSemesterPlansByFilterQueryHandler _handler = new GetSemesterPlansByFilterQueryHandler(repository);
	public async Task<OperationResult<IReadOnlyCollection<SemesterPlan>>> DoOperation() => await _handler.Handle(_query);
}

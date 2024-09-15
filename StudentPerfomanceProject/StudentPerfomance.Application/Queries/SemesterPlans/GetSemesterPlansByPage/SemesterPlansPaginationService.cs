using StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlansByPage;

public class SemesterPlansPaginationService
(
	int page,
	int pageSize,
	IRepositoryExpression<SemesterPlan> expression,
	IRepository<SemesterPlan> repository
)
: IService<IReadOnlyCollection<SemesterPlan>>
{
	private readonly GetSemesterPlansQuery _query = new GetSemesterPlansQuery(page, pageSize, expression);
	private readonly GetSemesterPlansQueryHandler _handler = new GetSemesterPlansQueryHandler(repository);
	public async Task<OperationResult<IReadOnlyCollection<SemesterPlan>>> DoOperation() => await _handler.Handle(_query);
}

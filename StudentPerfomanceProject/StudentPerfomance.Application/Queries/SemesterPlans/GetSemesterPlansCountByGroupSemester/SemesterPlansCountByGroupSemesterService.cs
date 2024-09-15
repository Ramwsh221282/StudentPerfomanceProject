using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlansCountByGroupSemester;

public sealed class SemesterPlansCountByGroupSemesterService
(
	IRepositoryExpression<SemesterPlan> expression,
	IRepository<SemesterPlan> repository
)
: IService<int>
{
	private readonly GetSemesterPlansCountByGroupSemesterQuery _query = new GetSemesterPlansCountByGroupSemesterQuery(expression);
	private readonly GetSemesterPlansCountByGroupSemesterQueryHandler _handler = new GetSemesterPlansCountByGroupSemesterQueryHandler(repository);
	public async Task<OperationResult<int>> DoOperation() => await _handler.Handle(_query);
}

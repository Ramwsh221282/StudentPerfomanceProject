using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlans;

internal sealed class GetSemesterPlansQueryHandler
(
	IRepository<SemesterPlan> repository
)
: IQueryHandler<GetSemesterPlansQuery, OperationResult<IReadOnlyCollection<SemesterPlan>>>
{
	private readonly IRepository<SemesterPlan> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<SemesterPlan>>> Handle(GetSemesterPlansQuery query)
	{
		return await this.ProcessAsync(async () =>
		{
			IReadOnlyCollection<SemesterPlan> plans = await _repository.GetPaged(query.Page, query.PageSize);
			return new OperationResult<IReadOnlyCollection<SemesterPlan>>(plans);
		});
	}
}

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlansByFilter;

internal sealed class GetSemesterPlansByFilterQueryHandler
(
	IRepository<SemesterPlan> repository
)
: IQueryHandler<GetSemesterPlansByFilterQuery, OperationResult<IReadOnlyCollection<SemesterPlan>>>
{
	private readonly IRepository<SemesterPlan> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<SemesterPlan>>> Handle(GetSemesterPlansByFilterQuery query)
	{
		return await this.ProcessAsync(async () =>
		{
			IReadOnlyCollection<SemesterPlan> plans = await _repository.GetFilteredAndPaged(query.Expression, query.Page, query.PageSize);
			return new OperationResult<IReadOnlyCollection<SemesterPlan>>(plans);
		});
	}
}

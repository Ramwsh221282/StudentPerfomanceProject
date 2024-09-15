using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlansCountByGroupSemester;

internal sealed class GetSemesterPlansCountByGroupSemesterQueryHandler
(
	IRepository<SemesterPlan> repository
)
: IQueryHandler<GetSemesterPlansCountByGroupSemesterQuery, OperationResult<int>>
{
	private readonly IRepository<SemesterPlan> _repository = repository;

	public async Task<OperationResult<int>> Handle(GetSemesterPlansCountByGroupSemesterQuery query)
	{
		return await this.ProcessAsync(async () =>
		{
			int count = await _repository.CountWithExpression(query.Expression);
			return new OperationResult<int>(count);
		});
	}
}

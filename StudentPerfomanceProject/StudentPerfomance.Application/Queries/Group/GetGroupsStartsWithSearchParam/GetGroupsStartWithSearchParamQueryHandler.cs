using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Group.GetGroupsStartsWithSearchParam;

internal sealed class GetGroupsStartWithSearchParamQueryHandler
(
	IRepository<StudentGroup> repository,
	IRepositoryExpression<StudentGroup> expression
)
: IQueryHandler<GetGroupsStartWithSearchParamQuery, OperationResult<IReadOnlyCollection<StudentGroup>>>
{
	private readonly IRepository<StudentGroup> _repository = repository;
	private readonly IRepositoryExpression<StudentGroup> _expression = expression;
	public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(GetGroupsStartWithSearchParamQuery query)
	{
		return await this.ProcessAsync(async () =>
		{
			IReadOnlyCollection<StudentGroup> groups = await _repository.GetFiltered(_expression);
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		});
	}
}

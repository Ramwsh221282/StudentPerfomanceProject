using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Group.GetGroupsByFilter;

internal sealed class GetGroupsByFilterQueryHandler
(
	IRepository<StudentGroup> repository,
	IRepositoryExpression<StudentGroup> expression
)
: IQueryHandler<GetGroupsByFilterQuery, OperationResult<IReadOnlyCollection<StudentGroup>>>
{
	private readonly IRepository<StudentGroup> _repository = repository;
	private readonly IRepositoryExpression<StudentGroup> _expression = expression;

	public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(GetGroupsByFilterQuery query)
	{
		return await this.ProcessAsync(async () =>
		{
			IReadOnlyCollection<StudentGroup> groups = await _repository.GetFilteredAndPaged(_expression, query.Page, query.PageSize);
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		});
	}
}

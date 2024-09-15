using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Group.GetGroupByName;

internal sealed class GetGroupByNameQueryHandler
(
	IRepository<StudentGroup> repository,
	IRepositoryExpression<StudentGroup> expression
)
: IQueryHandler<GetGroupByNameQuery, OperationResult<StudentGroup>>
{
	private readonly IRepository<StudentGroup> _repository = repository;
	private readonly IRepositoryExpression<StudentGroup> _expression = expression;
	public async Task<OperationResult<StudentGroup>> Handle(GetGroupByNameQuery query)
	{
		return await this.ProcessAsync(async () =>
		{
			StudentGroup? group = await _repository.GetByParameter(_expression);
			return new OperationResult<StudentGroup>(group);
		});
	}
}

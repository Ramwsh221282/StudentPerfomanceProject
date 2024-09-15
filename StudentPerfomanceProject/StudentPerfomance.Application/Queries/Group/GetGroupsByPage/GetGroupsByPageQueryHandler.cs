using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Group.GetGroupsByPage;

internal sealed class GetGroupsByPageQueryHandler
(
	IRepository<StudentGroup> repository
)
: IQueryHandler<GetGroupsByPageQuery, OperationResult<IReadOnlyCollection<StudentGroup>>>
{
	private readonly IRepository<StudentGroup> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(GetGroupsByPageQuery query)
	{
		return await this.ProcessAsync(async () =>
		{
			IReadOnlyCollection<StudentGroup> groups = await _repository.GetPaged(query.Page, query.PageSize);
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		});
	}
}

using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Queries.GetPaged;

public sealed class GetPagedTeachersQuery(int page, int pageSize, IRepository<Teacher> repository) : IQuery
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public readonly IQueryHandler<GetPagedTeachersQuery, IReadOnlyCollection<Teacher>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<Teacher> repository) : IQueryHandler<GetPagedTeachersQuery, IReadOnlyCollection<Teacher>>
	{
		private readonly IRepository<Teacher> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Teacher>>> Handle(GetPagedTeachersQuery query)
		{
			IReadOnlyCollection<Teacher> teachers = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
		}
	}
}

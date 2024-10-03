using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Queries.Paged;

public sealed class GetStudentGroupsPagedQuery(int page, int pageSize, IRepository<StudentGroup> repository) : IQuery
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public IQueryHandler<GetStudentGroupsPagedQuery, IReadOnlyCollection<StudentGroup>> Handler { get; init; } = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<StudentGroup> repository) : IQueryHandler<GetStudentGroupsPagedQuery, IReadOnlyCollection<StudentGroup>>
	{
		private readonly IRepository<StudentGroup> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(GetStudentGroupsPagedQuery query)
		{
			IReadOnlyCollection<StudentGroup> groups = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		}
	}
}

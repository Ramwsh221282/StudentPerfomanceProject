using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Queries.PagedFiltered;

public sealed class GetStudentGroupsPagedFilteredQuery(int page, int pageSize, IRepositoryExpression<StudentGroup> filter, IRepository<StudentGroup> repository) : IQuery
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepositoryExpression<StudentGroup> _filter = filter;
	public IQueryHandler<GetStudentGroupsPagedFilteredQuery, IReadOnlyCollection<StudentGroup>> Handler { get; init; } = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<StudentGroup> repository) : IQueryHandler<GetStudentGroupsPagedFilteredQuery, IReadOnlyCollection<StudentGroup>>
	{
		private readonly IRepository<StudentGroup> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(GetStudentGroupsPagedFilteredQuery query)
		{
			IReadOnlyCollection<StudentGroup> groups = await _repository.GetFilteredAndPaged(query._filter, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		}
	}
}

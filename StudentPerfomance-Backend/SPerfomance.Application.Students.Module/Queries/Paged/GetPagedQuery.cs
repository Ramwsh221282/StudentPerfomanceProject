using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Students.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Queries.Paged;

internal sealed class GetPagedQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly StudentQueryRepository _repository;
	public readonly IQueryHandler<GetPagedQuery, IReadOnlyCollection<Student>> Handler;
	public GetPagedQuery(int page, int pageSize)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new StudentQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(StudentQueryRepository repository) : IQueryHandler<GetPagedQuery, IReadOnlyCollection<Student>>
	{
		private readonly StudentQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Student>>> Handle(GetPagedQuery query)
		{
			IReadOnlyCollection<Student> students = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Student>>(students);
		}
	}
}

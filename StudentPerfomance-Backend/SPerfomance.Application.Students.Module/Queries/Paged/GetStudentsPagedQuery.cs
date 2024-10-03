using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Queries.Paged;

public sealed class GetStudentsPagedQuery(int page, int pageSize, IRepository<Student> repository) : IQuery
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public readonly IQueryHandler<GetStudentsPagedQuery, IReadOnlyCollection<Student>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<Student> repository) : IQueryHandler<GetStudentsPagedQuery, IReadOnlyCollection<Student>>
	{
		private readonly IRepository<Student> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Student>>> Handle(GetStudentsPagedQuery query)
		{
			IReadOnlyCollection<Student> students = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Student>>(students);
		}
	}
}

using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Queries.Filter;

public sealed class FilterStudentsQuery(int page, int pageSize, IRepositoryExpression<Student> filter, IRepository<Student> repository) : IQuery
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepositoryExpression<Student> _filter = filter;
	public readonly IQueryHandler<FilterStudentsQuery, IReadOnlyCollection<Student>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<Student> repository) : IQueryHandler<FilterStudentsQuery, IReadOnlyCollection<Student>>
	{
		private readonly IRepository<Student> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Student>>> Handle(FilterStudentsQuery query)
		{
			IReadOnlyCollection<Student> students = await _repository.GetFilteredAndPaged(query._filter, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Student>>(students);
		}
	}
}

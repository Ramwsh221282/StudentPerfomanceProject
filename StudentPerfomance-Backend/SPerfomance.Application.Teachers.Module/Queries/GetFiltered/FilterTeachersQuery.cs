using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Queries.GetFiltered;

public sealed class FilterTeachersQuery(int page, int pageSize, IRepositoryExpression<Teacher> expression, IRepository<Teacher> repository) : IQuery
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepositoryExpression<Teacher> _expression = expression;
	public readonly IQueryHandler<FilterTeachersQuery, IReadOnlyCollection<Teacher>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<Teacher> repository) : IQueryHandler<FilterTeachersQuery, IReadOnlyCollection<Teacher>>
	{
		private readonly IRepository<Teacher> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Teacher>>> Handle(FilterTeachersQuery query)
		{
			IReadOnlyCollection<Teacher> teachers = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
		}
	}
}

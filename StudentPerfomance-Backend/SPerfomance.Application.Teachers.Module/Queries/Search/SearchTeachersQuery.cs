using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Queries.Search;

public sealed class SearchTeachersQuery(IRepositoryExpression<Teacher> expression, IRepository<Teacher> repository) : IQuery
{
	private readonly IRepositoryExpression<Teacher> _expression = expression;
	public readonly IQueryHandler<SearchTeachersQuery, IReadOnlyCollection<Teacher>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<Teacher> repository) : IQueryHandler<SearchTeachersQuery, IReadOnlyCollection<Teacher>>
	{
		private readonly IRepository<Teacher> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Teacher>>> Handle(SearchTeachersQuery query)
		{
			IReadOnlyCollection<Teacher> teachers = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
		}
	}
}

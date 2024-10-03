using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.Semesters.Module.Queries.Search;

public sealed class SearchSemestersQuery(IRepositoryExpression<Semester> expression, IRepository<Semester> repository) : IQuery
{
	private readonly IRepositoryExpression<Semester> _expression = expression;
	public IQueryHandler<SearchSemestersQuery, IReadOnlyCollection<Semester>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<Semester> repository) : IQueryHandler<SearchSemestersQuery, IReadOnlyCollection<Semester>>
	{
		private readonly IRepository<Semester> _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<Semester>>> Handle(SearchSemestersQuery query)
		{
			IReadOnlyCollection<Semester> semesters = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<Semester>>(semesters);
		}
	}
}

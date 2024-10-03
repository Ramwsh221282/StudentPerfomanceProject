using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.Semesters.Module.Queries.GetPagedFiltered;

public sealed class FilterSemestersQuery(int page, int pageSize, IRepositoryExpression<Semester> expression, IRepository<Semester> repository) : IQuery
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepositoryExpression<Semester> _expression = expression;
	public readonly IQueryHandler<FilterSemestersQuery, IReadOnlyCollection<Semester>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<Semester> repository) : IQueryHandler<FilterSemestersQuery, IReadOnlyCollection<Semester>>
	{
		private readonly IRepository<Semester> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Semester>>> Handle(FilterSemestersQuery query)
		{
			IReadOnlyCollection<Semester> semesters = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Semester>>(semesters);
		}
	}
}

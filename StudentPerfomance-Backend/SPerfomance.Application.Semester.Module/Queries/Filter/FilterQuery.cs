using SPerfomance.Application.Semester.Module.Repository;
using SPerfomance.Application.Semester.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;

namespace SPerfomance.Application.Semester.Module.Queries.Filter;

internal sealed class FilterQuery : IQuery
{
	private readonly IRepositoryExpression<Domain.Module.Shared.Entities.Semesters.Semester> _expression;
	private readonly SemesterQueryRepository _repository;
	private readonly int _page;
	private readonly int _pageSize;
	public readonly IQueryHandler<FilterQuery, IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> Handler;
	public FilterQuery(SemesterSchema semester, int page, int pageSize)
	{
		_expression = ExpressionsFactory.Filter(semester.ToRepositoryObject());
		_page = page;
		_pageSize = pageSize;
		_repository = new SemesterQueryRepository();
		Handler = new QueryHandler(_repository);
	}
	internal sealed class QueryHandler(SemesterQueryRepository repository) : IQueryHandler<FilterQuery, IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>
	{
		private readonly SemesterQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>> Handle(FilterQuery query)
		{
			IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester> semesters = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>(semesters);
		}
	}
}

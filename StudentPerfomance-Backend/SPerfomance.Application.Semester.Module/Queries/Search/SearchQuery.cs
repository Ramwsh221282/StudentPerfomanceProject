using SPerfomance.Application.Semester.Module.Repository;
using SPerfomance.Application.Semester.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;

namespace SPerfomance.Application.Semester.Module.Queries.Search;

internal sealed class SearchQuery : IQuery
{
	private readonly IRepositoryExpression<Domain.Module.Shared.Entities.Semesters.Semester> _expression;
	private readonly SemesterQueryRepository _repository;
	public readonly IQueryHandler<SearchQuery, IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> Handler;
	public SearchQuery(SemesterSchema semester)
	{
		_expression = ExpressionsFactory.Filter(semester.ToRepositoryObject());
		_repository = new SemesterQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(SemesterQueryRepository repository) : IQueryHandler<SearchQuery, IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>
	{
		private readonly SemesterQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>> Handle(SearchQuery query)
		{
			IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester> semesters = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>(semesters);
		}
	}
}

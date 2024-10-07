using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Students.Module.Repository;
using SPerfomance.Application.Students.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Queries.Search;

internal sealed class SearchQuery : IQuery
{
	private readonly IRepositoryExpression<Student> _expression;
	private readonly StudentQueryRepository _repository;
	public readonly IQueryHandler<SearchQuery, IReadOnlyCollection<Student>> Handler;
	public SearchQuery(StudentSchema student)
	{
		_expression = ExpressionsFactory.Filter(student.ToRepositoryObject());
		_repository = new StudentQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(StudentQueryRepository repository) : IQueryHandler<SearchQuery, IReadOnlyCollection<Student>>
	{
		private readonly StudentQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Student>>> Handle(SearchQuery query)
		{
			IReadOnlyCollection<Student> students = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<Student>>(students);
		}
	}
}

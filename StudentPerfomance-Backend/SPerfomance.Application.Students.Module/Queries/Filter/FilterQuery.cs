using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Students.Module.Repository;
using SPerfomance.Application.Students.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Queries.Filter;

internal sealed class FilterQuery : IQuery
{
	private readonly StudentQueryRepository _repository;
	private readonly IRepositoryExpression<Student> _expression;
	private readonly int _page;
	private readonly int _pageSize;
	public readonly IQueryHandler<FilterQuery, IReadOnlyCollection<Student>> Handler;
	public FilterQuery(StudentSchema student, int page, int pageSize)
	{
		_expression = ExpressionsFactory.Filter(student.ToRepositoryObject());
		_page = page;
		_pageSize = pageSize;
		_repository = new StudentQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(StudentQueryRepository repository) : IQueryHandler<FilterQuery, IReadOnlyCollection<Student>>
	{
		private readonly StudentQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<Student>>> Handle(FilterQuery query)
		{
			IReadOnlyCollection<Student> students = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Student>>(students);
		}
	}
}

using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Application.Teachers.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Queries.Filtered;

internal sealed class FilterQuery : IQuery
{
	private readonly IRepositoryExpression<Teacher> _expression;
	private readonly int _page;
	private readonly int _pageSize;
	private readonly TeacherQueryRepository _repository;
	public readonly IQueryHandler<FilterQuery, IReadOnlyCollection<Teacher>> Handler;
	public FilterQuery(TeacherSchema teacher, int page, int pageSize)
	{
		_expression = ExpressionsFactory.Filter(teacher.ToRepositoryObject());
		_page = page;
		_pageSize = pageSize;
		_repository = new TeacherQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(TeacherQueryRepository repository) : IQueryHandler<FilterQuery, IReadOnlyCollection<Teacher>>
	{
		private readonly TeacherQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Teacher>>> Handle(FilterQuery query)
		{
			IReadOnlyCollection<Teacher> teachers = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
		}
	}
}

using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Application.StudentGroups.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Queries.Filter;

internal sealed class FilterQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly IRepositoryExpression<StudentGroup> _expression;
	private readonly StudentGroupQueryRepository _repository;
	public readonly IQueryHandler<FilterQuery, IReadOnlyCollection<StudentGroup>> Handler;
	public FilterQuery(StudentsGroupSchema group, int page, int pageSize)
	{
		_page = page;
		_pageSize = pageSize;
		_expression = ExpressionsFactory.Filter(group.ToRepositoryObject());
		_repository = new StudentGroupQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(StudentGroupQueryRepository repository) : IQueryHandler<FilterQuery, IReadOnlyCollection<StudentGroup>>
	{
		private readonly StudentGroupQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(FilterQuery query)
		{
			IReadOnlyCollection<StudentGroup> groups = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		}
	}
}

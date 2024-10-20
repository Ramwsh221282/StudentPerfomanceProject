using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Application.StudentGroups.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.StudentGroups.Module.Queries.Filter;

internal sealed class FilterQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly IRepositoryExpression<StudentGroup> _expression;
	private readonly StudentGroupQueryRepository _repository;

	public readonly IQueryHandler<FilterQuery, IReadOnlyCollection<StudentGroup>> Handler;

	public FilterQuery(StudentsGroupSchema group, int page, int pageSize, string token)
	{
		_page = page;
		_pageSize = pageSize;
		_expression = ExpressionsFactory.Filter(group.ToRepositoryObject());
		_repository = new StudentGroupQueryRepository();
		Handler = new QueryVerificaitonHandler<FilterQuery, IReadOnlyCollection<StudentGroup>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<FilterQuery, IReadOnlyCollection<StudentGroup>>
	{
		private readonly StudentGroupQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<FilterQuery, IReadOnlyCollection<StudentGroup>> handler,
			StudentGroupQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(FilterQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<StudentGroup> groups = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		}
	}
}

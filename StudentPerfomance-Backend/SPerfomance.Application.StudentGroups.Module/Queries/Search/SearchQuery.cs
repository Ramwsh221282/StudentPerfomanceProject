using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Application.StudentGroups.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.StudentGroups.Module.Queries.Search;

internal sealed class SearchQuery : IQuery
{
	private readonly IRepositoryExpression<StudentGroup> _expression;
	private readonly StudentGroupQueryRepository _repository;

	public readonly IQueryHandler<SearchQuery, IReadOnlyCollection<StudentGroup>> Handler;

	public SearchQuery(StudentsGroupSchema group, string token)
	{
		_expression = ExpressionsFactory.Filter(group.ToRepositoryObject());
		_repository = new StudentGroupQueryRepository();
		Handler = new QueryVerificaitonHandler<SearchQuery, IReadOnlyCollection<StudentGroup>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<SearchQuery, IReadOnlyCollection<StudentGroup>>
	{
		private readonly StudentGroupQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<SearchQuery, IReadOnlyCollection<StudentGroup>> handler,
			StudentGroupQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(SearchQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<StudentGroup> groups = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		}
	}
}

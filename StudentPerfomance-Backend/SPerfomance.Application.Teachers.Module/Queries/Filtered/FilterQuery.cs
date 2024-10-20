using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Application.Teachers.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Teachers.Module.Queries.Filtered;

internal sealed class FilterQuery : IQuery
{
	private readonly IRepositoryExpression<Teacher> _expression;
	private readonly int _page;
	private readonly int _pageSize;
	private readonly TeacherQueryRepository _repository;

	public readonly IQueryHandler<FilterQuery, IReadOnlyCollection<Teacher>> Handler;

	public FilterQuery(TeacherSchema teacher, int page, int pageSize, string token)
	{
		_expression = ExpressionsFactory.Filter(teacher.ToRepositoryObject());
		_page = page;
		_pageSize = pageSize;
		_repository = new TeacherQueryRepository();
		Handler = new QueryVerificaitonHandler<FilterQuery, IReadOnlyCollection<Teacher>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<FilterQuery, IReadOnlyCollection<Teacher>>
	{
		private readonly TeacherQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<FilterQuery, IReadOnlyCollection<Teacher>> handler,
			TeacherQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<Teacher>>> Handle(FilterQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<Teacher> teachers = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
		}
	}
}

using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Application.Teachers.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Teachers.Module.Queries.Searched;

internal sealed class SearchQuery : IQuery
{
	private readonly IRepositoryExpression<Teacher> _expression;
	private readonly TeacherQueryRepository _repository;

	public readonly IQueryHandler<SearchQuery, IReadOnlyCollection<Teacher>> Handler;

	public SearchQuery(TeacherSchema teacher, string token)
	{
		_expression = ExpressionsFactory.Filter(teacher.ToRepositoryObject());
		_repository = new TeacherQueryRepository();
		Handler = new QueryVerificaitonHandler<SearchQuery, IReadOnlyCollection<Teacher>>(token, User.Teacher);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<SearchQuery, IReadOnlyCollection<Teacher>>
	{
		private readonly TeacherQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<SearchQuery, IReadOnlyCollection<Teacher>> handler,
			TeacherQueryRepository repository)
			 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<Teacher>>> Handle(SearchQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<Teacher> teachers = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
		}
	}
}

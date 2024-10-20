using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.Students.Module.Repository;
using SPerfomance.Application.Students.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Students.Module.Queries.Filter;

internal sealed class FilterQuery : IQuery
{
	private readonly StudentQueryRepository _repository;
	private readonly IRepositoryExpression<Student> _expression;
	private readonly int _page;
	private readonly int _pageSize;

	public readonly IQueryHandler<FilterQuery, IReadOnlyCollection<Student>> Handler;

	public FilterQuery(StudentSchema student, int page, int pageSize, string token)
	{
		_expression = ExpressionsFactory.Filter(student.ToRepositoryObject());
		_page = page;
		_pageSize = pageSize;
		_repository = new StudentQueryRepository();
		Handler = new QueryVerificaitonHandler<FilterQuery, IReadOnlyCollection<Student>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<FilterQuery, IReadOnlyCollection<Student>>
	{
		private readonly StudentQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<FilterQuery, IReadOnlyCollection<Student>> handler,
			StudentQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<Student>>> Handle(FilterQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<Student> students = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Student>>(students);
		}
	}
}

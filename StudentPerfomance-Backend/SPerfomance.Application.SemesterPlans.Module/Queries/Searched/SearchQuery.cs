using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.SemesterPlans.Module.Queries.Searched;

internal sealed class SearchQuery : IQuery
{
	private readonly IRepositoryExpression<SemesterPlan> _expression;
	private readonly SemesterPlansQueryRepository _repository;

	public IQueryHandler<SearchQuery, IReadOnlyCollection<SemesterPlan>> Handler;

	public SearchQuery(SemesterPlanSchema schema, string token)
	{
		_expression = ExpressionsFactory.Filter(schema.ToRepositoryObject());
		_repository = new SemesterPlansQueryRepository();
		Handler = new QueryVerificaitonHandler<SearchQuery, IReadOnlyCollection<SemesterPlan>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<SearchQuery, IReadOnlyCollection<SemesterPlan>>
	{
		private readonly SemesterPlansQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<SearchQuery, IReadOnlyCollection<SemesterPlan>> handler,
			SemesterPlansQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<SemesterPlan>>> Handle(SearchQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<SemesterPlan> plans = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<SemesterPlan>>(plans);
		}
	}
}

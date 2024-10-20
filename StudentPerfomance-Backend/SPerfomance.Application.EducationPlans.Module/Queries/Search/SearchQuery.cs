using SPerfomance.Application.EducationPlans.Module.Repository;
using SPerfomance.Application.EducationPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.DTOs.EducationPlans;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.EducationPlans.Module.Queries.Search;

internal sealed class SearchQuery : IQuery
{
	private readonly IRepositoryExpression<EducationPlan> _expression;
	private readonly EducationPlanQueryRepository _repository;

	public IQueryHandler<SearchQuery, IReadOnlyCollection<EducationPlan>> Handler;

	public SearchQuery(EducationPlanDTO plan, string token)
	{
		_expression = ExpressionsFactory.Filter(plan.ToSchema().ToRepositoryObject());
		_repository = new EducationPlanQueryRepository();
		Handler = new QueryVerificaitonHandler<SearchQuery, IReadOnlyCollection<EducationPlan>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}
	internal sealed class QueryHandler : DecoratedQueryHandler<SearchQuery, IReadOnlyCollection<EducationPlan>>
	{
		private readonly EducationPlanQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<SearchQuery, IReadOnlyCollection<EducationPlan>> handler,
			EducationPlanQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> Handle(SearchQuery query)
		{
			OperationResult<IReadOnlyCollection<EducationPlan>> result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<EducationPlan> plans = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
		}
	}
}

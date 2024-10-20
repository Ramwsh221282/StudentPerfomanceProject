using SPerfomance.Application.EducationPlans.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.EducationPlans.Module.Queries.GetPaged;

internal sealed class GetPagedQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly EducationPlanQueryRepository _repository;

	public IQueryHandler<GetPagedQuery, IReadOnlyCollection<EducationPlan>> Handler;

	public GetPagedQuery(int page, int pageSize, string token)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new EducationPlanQueryRepository();
		Handler = new QueryVerificaitonHandler<GetPagedQuery, IReadOnlyCollection<EducationPlan>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetPagedQuery, IReadOnlyCollection<EducationPlan>>
	{
		private readonly EducationPlanQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetPagedQuery, IReadOnlyCollection<EducationPlan>> handler,
			EducationPlanQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> Handle(GetPagedQuery query)
		{
			OperationResult<IReadOnlyCollection<EducationPlan>> result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<EducationPlan> plans = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
		}
	}
}

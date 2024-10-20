using SPerfomance.Application.EducationPlans.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.EducationPlans.Module.Queries.All;

internal sealed class GetAllQuery : IQuery
{
	private readonly EducationPlanQueryRepository _repository;
	public readonly IQueryHandler<GetAllQuery, IReadOnlyCollection<EducationPlan>> Handler;

	public GetAllQuery(string token)
	{
		_repository = new EducationPlanQueryRepository();
		Handler = new QueryVerificaitonHandler<GetAllQuery, IReadOnlyCollection<EducationPlan>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetAllQuery, IReadOnlyCollection<EducationPlan>>
	{
		private readonly EducationPlanQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetAllQuery, IReadOnlyCollection<EducationPlan>> handler,
			EducationPlanQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> Handle(GetAllQuery query)
		{
			OperationResult<IReadOnlyCollection<EducationPlan>> result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<EducationPlan> plans = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
		}
	}
}

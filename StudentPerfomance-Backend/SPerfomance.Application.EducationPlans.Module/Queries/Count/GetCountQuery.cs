using SPerfomance.Application.EducationPlans.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.EducationPlans.Module.Queries.Count;

internal sealed class GetCountQuery : IQuery
{
	private readonly EducationPlanQueryRepository _repository;

	public readonly IQueryHandler<GetCountQuery, int> Handler;

	public GetCountQuery(string token)
	{
		_repository = new EducationPlanQueryRepository();
		Handler = new QueryVerificaitonHandler<GetCountQuery, int>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetCountQuery, int>
	{
		private readonly EducationPlanQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetCountQuery, int> handler,
			EducationPlanQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<int>> Handle(GetCountQuery query)
		{
			OperationResult<int> result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}

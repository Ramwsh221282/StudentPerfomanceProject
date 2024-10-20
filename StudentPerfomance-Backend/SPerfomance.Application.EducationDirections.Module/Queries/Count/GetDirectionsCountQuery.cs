using SPerfomance.Application.EducationDirections.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.EducationDirections.Module.Queries.Count;

internal sealed class GetDirectionsCountQuery : IQuery
{
	private readonly EducationDirectionsQueryRepository _repository;
	public readonly IQueryHandler<GetDirectionsCountQuery, int> Handler;

	public GetDirectionsCountQuery(string token)
	{
		_repository = new EducationDirectionsQueryRepository();
		Handler = new QueryVerificaitonHandler<GetDirectionsCountQuery, int>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetDirectionsCountQuery, int>
	{
		private readonly EducationDirectionsQueryRepository _repository;

		public QueryHandler(IQueryHandler<GetDirectionsCountQuery, int> handler, EducationDirectionsQueryRepository repository)
		 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<int>> Handle(GetDirectionsCountQuery query)
		{
			OperationResult<int> result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}

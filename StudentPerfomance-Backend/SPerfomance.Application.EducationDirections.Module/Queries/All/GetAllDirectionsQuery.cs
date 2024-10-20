using SPerfomance.Application.EducationDirections.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.EducationDirections.Module.Queries.All;

internal sealed class GetAllDirectionsQuery : IQuery
{
	private readonly EducationDirectionsQueryRepository _repository;
	public IQueryHandler<GetAllDirectionsQuery, IReadOnlyCollection<EducationDirection>> Handler;

	public GetAllDirectionsQuery(string token)
	{
		_repository = new EducationDirectionsQueryRepository();
		Handler = new QueryVerificaitonHandler<GetAllDirectionsQuery, IReadOnlyCollection<EducationDirection>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal abstract class QueryDecorator(IQueryHandler<GetAllDirectionsQuery, IReadOnlyCollection<EducationDirection>> handler)
	: IQueryHandler<GetAllDirectionsQuery, IReadOnlyCollection<EducationDirection>>
	{
		private readonly IQueryHandler<GetAllDirectionsQuery, IReadOnlyCollection<EducationDirection>> _handler = handler;

		public virtual async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Handle(GetAllDirectionsQuery query) =>
			await _handler.Handle(query);
	}

	internal sealed class QueryHandler : QueryDecorator
	{
		private readonly EducationDirectionsQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetAllDirectionsQuery, IReadOnlyCollection<EducationDirection>> handler,
			EducationDirectionsQueryRepository repository
			)
		 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Handle(GetAllDirectionsQuery query)
		{
			OperationResult<IReadOnlyCollection<EducationDirection>> result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<EducationDirection> directions = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
		}
	}
}

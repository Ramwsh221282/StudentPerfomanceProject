using SPerfomance.Application.EducationDirections.Module.Repository;
using SPerfomance.Application.EducationDirections.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.DTOs.EducationDirections;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.EducationDirections.Module.Queries.Search;

internal sealed class SearchQuery : IQuery
{
	private readonly IRepositoryExpression<EducationDirection> _expression;
	private readonly EducationDirectionsQueryRepository _repository;

	public readonly IQueryHandler<SearchQuery, IReadOnlyCollection<EducationDirection>> Handler;

	public SearchQuery(EducationDirectionDTO direction, string token)
	{
		_expression = ExpressionsFactory.Filter(direction.ToSchema().ToRepositoryObject());
		_repository = new EducationDirectionsQueryRepository();
		Handler = new QueryVerificaitonHandler<SearchQuery, IReadOnlyCollection<EducationDirection>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<SearchQuery, IReadOnlyCollection<EducationDirection>>
	{
		private readonly EducationDirectionsQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<SearchQuery, IReadOnlyCollection<EducationDirection>> handler,
			EducationDirectionsQueryRepository repository)
			 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Handle(SearchQuery query)
		{
			OperationResult<IReadOnlyCollection<EducationDirection>> result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<EducationDirection> directions = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
		}
	}
}

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

namespace SPerfomance.Application.EducationDirections.Module.Queries.Filter;

internal sealed class FilterQuery : IQuery
{
	private readonly IRepositoryExpression<EducationDirection> _expression;
	private readonly int _page;
	private readonly int _pageSize;
	private readonly EducationDirectionsQueryRepository _repository;

	public readonly IQueryHandler<FilterQuery, IReadOnlyCollection<EducationDirection>> Handler;

	public FilterQuery(EducationDirectionDTO direction, int page, int pageSize, string token)
	{
		_expression = ExpressionsFactory.Filter(direction.ToSchema().ToRepositoryObject());
		_page = page;
		_pageSize = pageSize;
		_repository = new EducationDirectionsQueryRepository();
		Handler = new QueryVerificaitonHandler<FilterQuery, IReadOnlyCollection<EducationDirection>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<FilterQuery, IReadOnlyCollection<EducationDirection>>
	{
		private readonly EducationDirectionsQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<FilterQuery, IReadOnlyCollection<EducationDirection>> handler,
			EducationDirectionsQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Handle(FilterQuery query)
		{
			OperationResult<IReadOnlyCollection<EducationDirection>> result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<EducationDirection> directions = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
		}
	}
}

using SPerfomance.Application.Shared.Module.Operations;

namespace SPerfomance.Application.Shared.Module.CQRS.Queries;

public abstract class DecoratedQueryHandler<TQuery, TQueryResult> : IQueryHandler<TQuery, TQueryResult>
where TQuery : IQuery
{
	private readonly IQueryHandler<TQuery, TQueryResult> _handler;

	public DecoratedQueryHandler(IQueryHandler<TQuery, TQueryResult> handler)
	{
		_handler = handler;
	}

	public virtual async Task<OperationResult<TQueryResult>> Handle(TQuery query) => await _handler.Handle(query);
}

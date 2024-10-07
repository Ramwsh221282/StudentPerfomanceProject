using SPerfomance.Application.Shared.Module.Operations;

namespace SPerfomance.Application.Shared.Module.CQRS.Queries;

public interface IQueryHandler<TQuery, TQueryResult> where TQuery : IQuery
{
	Task<OperationResult<TQueryResult>> Handle(TQuery query);
}

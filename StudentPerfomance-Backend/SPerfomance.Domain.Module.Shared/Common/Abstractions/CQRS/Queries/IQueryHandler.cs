using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;

namespace SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;

public interface IQueryHandler<TQuery, TQueryResult> where TQuery : IQuery
{
	Task<OperationResult<TQueryResult>> Handle(TQuery query);
}

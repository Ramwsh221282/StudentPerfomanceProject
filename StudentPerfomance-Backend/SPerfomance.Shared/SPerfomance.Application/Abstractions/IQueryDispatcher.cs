using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Abstractions;

public interface IQueryDispatcher
{
    public Task<Result<TResult>> Dispatch<TQuery, TResult>(
        TQuery query,
        CancellationToken ct = default
    )
        where TQuery : IQuery<TResult>
        where TResult : class;
}

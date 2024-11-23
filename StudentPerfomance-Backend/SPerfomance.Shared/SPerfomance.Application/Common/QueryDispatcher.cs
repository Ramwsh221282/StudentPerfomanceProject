using Microsoft.Extensions.DependencyInjection;
using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Common;

public sealed class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    public async Task<Result<TResult>> Dispatch<TQuery, TResult>(
        TQuery query,
        CancellationToken ct = default
    )
        where TQuery : IQuery<TResult>
        where TResult : class
    {
        var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await handler.Handle(query, ct);
    }
}

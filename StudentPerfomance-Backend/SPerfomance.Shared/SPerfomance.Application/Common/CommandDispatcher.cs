using Microsoft.Extensions.DependencyInjection;
using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Common;

public sealed class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    public async Task<Result<TResult>> Dispatch<TCommand, TResult>(
        TCommand command,
        CancellationToken ct = default
    )
        where TCommand : ICommand<TResult>
        where TResult : class
    {
        var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return await handler.Handle(command, ct);
    }
}

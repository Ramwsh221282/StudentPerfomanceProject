using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Abstractions;

public interface ICommandDispatcher
{
    public Task<Result<TResult>> Dispatch<TCommand, TResult>(
        TCommand command,
        CancellationToken ct = default
    )
        where TCommand : ICommand<TResult>
        where TResult : class;
}

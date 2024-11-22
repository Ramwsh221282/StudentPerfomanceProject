using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Abstractions;

public interface ICommandHandler<in TCommand, TResult>
    where TCommand : ICommand<TResult>
    where TResult : class
{
    Task<Result<TResult>> Handle(TCommand command, CancellationToken ct = default);
}

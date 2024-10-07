using SPerfomance.Application.Shared.Module.Operations;

namespace SPerfomance.Application.Shared.Module.CQRS.Commands;

public interface ICommandHandler<TCommand, TCommandResult>
where TCommand : ICommand
{
	Task<OperationResult<TCommandResult>> Handle(TCommand command);
}

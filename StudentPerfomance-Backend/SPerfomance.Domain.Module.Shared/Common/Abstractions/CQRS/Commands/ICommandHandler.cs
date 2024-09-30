namespace SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;

public interface ICommandHandler<TCommand, TCommandResult>
where TCommand : ICommand
{
	Task<TCommandResult> Handle(TCommand command);
}

namespace StudentPerfomance.Application.Commands;

internal interface ICommandHandler<TCommand, TCommandResult>
where TCommand : ICommand
{
	Task<TCommandResult> Handle(TCommand command);
}

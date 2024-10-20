using SPerfomance.Application.Shared.Module.Operations;

namespace SPerfomance.Application.Shared.Module.CQRS.Commands;

public abstract class DecoratedCommandHandler<TCommand, TCommandReuslt> : ICommandHandler<TCommand, TCommandReuslt>
where TCommand : ICommand
{
	private readonly ICommandHandler<TCommand, TCommandReuslt> _handler;

	public DecoratedCommandHandler(ICommandHandler<TCommand, TCommandReuslt> handler)
	{
		_handler = handler;
	}

	public virtual async Task<OperationResult<TCommandReuslt>> Handle(TCommand command) => await _handler.Handle(command);
}

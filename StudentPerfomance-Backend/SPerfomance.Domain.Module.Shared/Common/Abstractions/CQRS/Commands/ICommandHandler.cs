using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;

namespace SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;

public interface ICommandHandler<TCommand, TCommandResult>
where TCommand : ICommand
{
	Task<OperationResult<TCommandResult>> Handle(TCommand command);
}

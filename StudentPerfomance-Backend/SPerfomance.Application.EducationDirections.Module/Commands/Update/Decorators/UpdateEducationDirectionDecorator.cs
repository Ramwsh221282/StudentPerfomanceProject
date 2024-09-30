using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Update.Decorators;

internal class UpdateEducationDirectionDecorator
(
	ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> handler
) : ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>>
{
	private readonly ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> _handler = handler;
	public virtual async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionCommand command) => await _handler.Handle(command);
}

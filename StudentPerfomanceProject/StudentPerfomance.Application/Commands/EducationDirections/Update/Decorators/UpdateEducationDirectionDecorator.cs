using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Application.Commands.EducationDirections.Update.Decorators;

internal class UpdateEducationDirectionDecorator
(
	ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> handler
) : ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>>
{
	private readonly ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> _handler = handler;
	public virtual async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionCommand command) => await _handler.Handle(command);
}

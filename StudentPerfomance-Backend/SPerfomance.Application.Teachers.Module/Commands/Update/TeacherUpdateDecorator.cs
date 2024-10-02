using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Commands.Update;

public class TeacherUpdateDecorator
(ICommandHandler<TeacherUpdateCommand, OperationResult<Teacher>> handler)
: ICommandHandler<TeacherUpdateCommand, OperationResult<Teacher>>
{
	private readonly ICommandHandler<TeacherUpdateCommand, OperationResult<Teacher>> _handler = handler;
	public virtual async Task<OperationResult<Teacher>> Handle(TeacherUpdateCommand command) => await _handler.Handle(command);
}

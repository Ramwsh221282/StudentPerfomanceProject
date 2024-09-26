using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Application.Commands.EducationDirections.Update.Decorators;

internal sealed class UpdateEducationDirectionNameHandler : UpdateEducationDirectionDecorator
{
	public UpdateEducationDirectionNameHandler(ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> handler)
	: base(handler) { }

	public override async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionCommand command)
	{
		OperationResult<EducationDirection> result = await base.Handle(command);
		if (result.IsFailed) return result;
		result.Result.ChangeDirectionName(command.NewSchema.CreateDirectionName());
		return new OperationResult<EducationDirection>(result.Result);
	}
}

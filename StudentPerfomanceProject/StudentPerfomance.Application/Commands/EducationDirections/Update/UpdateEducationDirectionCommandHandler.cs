using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Application.Commands.EducationDirections.Update;

internal abstract class UpdateEducationDirectionCommandHandler
(
	ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> handler
) : CommandWithErrorBuilder, ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>>
{
	private readonly ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> _handler = handler;
	public async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionCommand command)
	{
		return await this.ProcessAsync(async () =>
		{
			return await _handler.Handle(command);
		});
	}
}

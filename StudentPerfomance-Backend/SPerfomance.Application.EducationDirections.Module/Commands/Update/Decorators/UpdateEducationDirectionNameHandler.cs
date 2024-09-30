using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Update.Decorators;

internal sealed class UpdateEducationDirectionNameHandler : UpdateEducationDirectionDecorator
{
	public UpdateEducationDirectionNameHandler(ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> handler)
	: base(handler) { }

	public override async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionCommand command)
	{
		OperationResult<EducationDirection> result = await base.Handle(command);
		if (result.Result == null || result.IsFailed) return result;
		if (result.Result.Name.Name == command.NewSchema.Name) return result;
		result.Result.ChangeDirectionName(command.NewSchema.CreateDirectionName());
		return new OperationResult<EducationDirection>(result.Result);
	}
}

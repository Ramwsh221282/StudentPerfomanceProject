using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Update.Decorators;

internal sealed class UpdateEducationDirectionCodeHandler : UpdateEducationDirectionDecorator
{
	private readonly IRepository<EducationDirection> _repository;
	public UpdateEducationDirectionCodeHandler
	(
		ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> handler,
		IRepository<EducationDirection> repository
	)
	: base(handler) => _repository = repository;

	public override async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionCommand command)
	{
		OperationResult<EducationDirection> result = await base.Handle(command);
		if (result.Result == null || result.IsFailed) return result;
		if (result.Result.Code.Code == command.NewSchema.Code) return result;
		if (await _repository.HasEqualRecord(command.CheckForCodeDublicate))
			return new OperationResult<EducationDirection>(new EducationDirectionCodeDublicateError(command.NewSchema.Code).ToString());
		result.Result.ChangeDirectionCode(command.NewSchema.CreateDirectionCode());
		return new OperationResult<EducationDirection>(result.Result);
	}
}

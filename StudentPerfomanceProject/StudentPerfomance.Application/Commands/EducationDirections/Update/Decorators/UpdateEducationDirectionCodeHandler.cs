using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationDirections.Update.Decorators;

internal sealed class UpdateEducationDirectionCodeHandler : UpdateEducationDirectionDecorator
{
	private readonly IRepository<EducationDirection> _repository;
	public UpdateEducationDirectionCodeHandler(
		ICommandHandler<UpdateEducationDirectionCommand,
		OperationResult<EducationDirection>> handler,
		IRepository<EducationDirection> repository
		)
	: base(handler) => _repository = repository;

	public override async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionCommand command)
	{
		OperationResult<EducationDirection> result = await base.Handle(command);
		if (result.IsFailed) return result;
		if (result.Result.Code.Code != command.NewSchema.Code)
		{
			if (await _repository.HasEqualRecord(command.CheckForCodeDublicate))
				return new OperationResult<EducationDirection>("Направление подготовки с таким кодом существует");
			result.Result.ChangeDirectionCode(command.NewSchema.CreateDirectionCode());
		}
		return new OperationResult<EducationDirection>(result.Result);
	}
}

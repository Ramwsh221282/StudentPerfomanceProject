using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationDirections.Commands.UpdateEducationDirection;

public class UpdateEducationDirectionCommandHandler
(IEducationDirectionRepository repository)
: ICommandHandler<UpdateEducationDirectionCommand, EducationDirection>
{
	private readonly IEducationDirectionRepository _repository = repository;

	public async Task<Result<EducationDirection>> Handle(UpdateEducationDirectionCommand command)
	{
		if (command.Direction == null)
			return Result<EducationDirection>.Failure(EducationDirectionErrors.NotFoundError());

		Result<EducationDirection> updated = command.Direction.ChangeName(command.NewName);
		if (updated.IsFailure)
			return updated;

		if (updated.Value.Code.Code != command.NewCode)
		{
			if (await _repository.HasWithCode(command.NewCode))
				return Result<EducationDirection>.Failure(EducationDirectionErrors.CodeDublicateError(command.NewCode));
			updated = command.Direction.ChangeCode(command.NewCode);
			if (updated.IsFailure)
				return updated;
		}

		updated = command.Direction.ChangeType(command.NewType);
		if (updated.IsFailure)
			return updated;

		await _repository.Update(updated.Value);
		return Result<EducationDirection>.Success(updated.Value);
	}
}

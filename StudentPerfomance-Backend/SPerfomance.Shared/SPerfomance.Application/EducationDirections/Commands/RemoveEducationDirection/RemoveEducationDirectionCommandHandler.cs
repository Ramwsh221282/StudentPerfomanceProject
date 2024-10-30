using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationDirections.Commands.RemoveEducationDirection;

public class RemoveEducationDirectionCommandHandler
(IEducationDirectionRepository repository)
: ICommandHandler<RemoveEducationDirectionCommand, EducationDirection>
{
	private readonly IEducationDirectionRepository _repository = repository;

	public async Task<Result<EducationDirection>> Handle(RemoveEducationDirectionCommand command)
	{
		if (command.Direction == null)
			return Result<EducationDirection>.Failure(EducationDirectionErrors.NotFoundError());

		await _repository.Remove(command.Direction);
		return Result<EducationDirection>.Success(command.Direction);
	}
}

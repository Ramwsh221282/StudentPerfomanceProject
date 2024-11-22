using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationDirections.Commands.RemoveEducationDirection;

public class RemoveEducationDirectionCommandHandler(IEducationDirectionRepository repository)
    : ICommandHandler<RemoveEducationDirectionCommand, EducationDirection>
{
    public async Task<Result<EducationDirection>> Handle(
        RemoveEducationDirectionCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Direction == null)
            return Result<EducationDirection>.Failure(EducationDirectionErrors.NotFoundError());

        await repository.Remove(command.Direction, ct);
        return Result<EducationDirection>.Success(command.Direction);
    }
}

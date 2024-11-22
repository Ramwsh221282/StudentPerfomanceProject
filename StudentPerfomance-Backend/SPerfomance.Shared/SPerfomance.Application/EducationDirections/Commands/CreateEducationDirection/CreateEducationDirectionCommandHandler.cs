using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationDirections.Commands.CreateEducationDirection;

public class CreateEducationDirectionCommandHandler(IEducationDirectionRepository repository)
    : ICommandHandler<CreateEducationDirectionCommand, EducationDirection>
{
    public async Task<Result<EducationDirection>> Handle(
        CreateEducationDirectionCommand command,
        CancellationToken ct = default
    )
    {
        var creation = EducationDirection.Create(command.Code, command.Name, command.Type);
        if (creation.IsFailure)
            return creation;

        var direction = creation.Value;
        if (await repository.HasWithCode(direction.Code.Code, ct))
            return Result<EducationDirection>.Failure(
                EducationDirectionErrors.CodeDublicateError(direction.Code.Code)
            );

        if (await repository.Has(direction.Code.Code, direction.Name.Name, direction.Type.Type, ct))
            return Result<EducationDirection>.Failure(
                EducationDirectionErrors.DirectionDublicateError(
                    direction.Code.Code,
                    direction.Name.Name,
                    direction.Type.Type
                )
            );

        await repository.Insert(direction, ct);
        return Result<EducationDirection>.Success(direction);
    }
}

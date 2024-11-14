using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationDirections.Commands.CreateEducationDirection;

public class CreateEducationDirectionCommandHandler(IEducationDirectionRepository repository)
    : ICommandHandler<CreateEducationDirectionCommand, EducationDirection>
{
    private readonly IEducationDirectionRepository _repository = repository;

    public async Task<Result<EducationDirection>> Handle(CreateEducationDirectionCommand command)
    {
        Result<EducationDirection> creation = EducationDirection.Create(
            command.Code,
            command.Name,
            command.Type
        );
        if (creation.IsFailure)
            return creation;

        EducationDirection direction = creation.Value;
        if (await _repository.HasWithCode(direction.Code.Code))
            return Result<EducationDirection>.Failure(
                EducationDirectionErrors.CodeDublicateError(direction.Code.Code)
            );

        if (await _repository.Has(direction.Code.Code, direction.Name.Name, direction.Type.Type))
            return Result<EducationDirection>.Failure(
                EducationDirectionErrors.DirectionDublicateError(
                    direction.Code.Code,
                    direction.Name.Name,
                    direction.Type.Type
                )
            );

        await _repository.Insert(direction);
        return Result<EducationDirection>.Success(direction);
    }
}

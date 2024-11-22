using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;

public class GetEducationDirectionQueryHandler(IEducationDirectionRepository repository)
    : IQueryHandler<GetEducationDirectionQuery, EducationDirection>
{
    public async Task<Result<EducationDirection>> Handle(
        GetEducationDirectionQuery command,
        CancellationToken ct = default
    )
    {
        var requested = await repository.Get(command.Code, command.Name, command.Type, ct);
        return requested == null
            ? Result<EducationDirection>.Failure(EducationDirectionErrors.NotFoundError())
            : Result<EducationDirection>.Success(requested);
    }
}

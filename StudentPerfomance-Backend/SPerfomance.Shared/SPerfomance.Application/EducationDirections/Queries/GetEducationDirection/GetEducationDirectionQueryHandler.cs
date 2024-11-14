using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;

public class GetEducationDirectionQueryHandler(IEducationDirectionRepository repository)
    : IQueryHandler<GetEducationDirectionQuery, EducationDirection>
{
    private readonly IEducationDirectionRepository _repository = repository;

    public async Task<Result<EducationDirection>> Handle(GetEducationDirectionQuery command)
    {
        EducationDirection? requested = await _repository.Get(
            command.Code,
            command.Name,
            command.Type
        );
        return requested == null
            ? Result<EducationDirection>.Failure(EducationDirectionErrors.NotFoundError())
            : Result<EducationDirection>.Success(requested);
    }
}

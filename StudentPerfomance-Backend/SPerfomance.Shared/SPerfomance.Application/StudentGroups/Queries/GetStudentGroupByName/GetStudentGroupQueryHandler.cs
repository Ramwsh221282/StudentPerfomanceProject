using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;

public class GetStudentGroupQueryHandler(IStudentGroupsRepository repository)
    : IQueryHandler<GetStudentGroupQuery, StudentGroup>
{
    public async Task<Result<StudentGroup>> Handle(
        GetStudentGroupQuery command,
        CancellationToken ct = default
    )
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            return Result<StudentGroup>.Failure(StudentGroupErrors.NameEmpty());

        StudentGroup? requested = await repository.Get(command.Name, ct);
        return requested == null
            ? Result<StudentGroup>.Failure(StudentGroupErrors.NotFound())
            : Result<StudentGroup>.Success(requested);
    }
}

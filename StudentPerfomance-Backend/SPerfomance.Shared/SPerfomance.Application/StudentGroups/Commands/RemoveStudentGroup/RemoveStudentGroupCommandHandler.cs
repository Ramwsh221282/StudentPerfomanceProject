using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.RemoveStudentGroup;

public class RemoveStudentGroupCommandHandler(IStudentGroupsRepository repository)
    : ICommandHandler<RemoveStudentGroupCommand, StudentGroup>
{
    public async Task<Result<StudentGroup>> Handle(
        RemoveStudentGroupCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Group == null)
            return Result<StudentGroup>.Failure(StudentGroupErrors.NotFound());

        await repository.Remove(command.Group, ct);
        return Result<StudentGroup>.Success(command.Group);
    }
}

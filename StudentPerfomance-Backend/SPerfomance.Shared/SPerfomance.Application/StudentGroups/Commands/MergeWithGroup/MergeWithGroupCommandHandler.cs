using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.MergeWithGroup;

public class MergeWithGroupCommandHandler(IStudentGroupsRepository repository)
    : ICommandHandler<MergeWithGroupCommand, StudentGroup>
{
    public async Task<Result<StudentGroup>> Handle(
        MergeWithGroupCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Initial == null || command.Target == null)
            return Result<StudentGroup>.Failure(StudentGroupErrors.NotFound());

        var mergedGroup = command.Initial.MergeWithGroup(command.Target);
        await repository.UpdateMerge(mergedGroup.Value, command.Target, ct);
        return Result<StudentGroup>.Success(mergedGroup.Value);
    }
}

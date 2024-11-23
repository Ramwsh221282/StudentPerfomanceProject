using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.ChangeGroupName;

public class ChangeGroupNameCommandHandler(IStudentGroupsRepository repository)
    : ICommandHandler<ChangeGroupNameCommand, StudentGroup>
{
    public async Task<Result<StudentGroup>> Handle(
        ChangeGroupNameCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Group == null)
            return Result<StudentGroup>.Failure(StudentGroupErrors.NotFound());

        if (string.IsNullOrWhiteSpace(command.NewName))
            return StudentGroupErrors.NameEmpty();

        if (await repository.HasWithName(command.NewName, ct))
            return StudentGroupErrors.NameDublicate(command.NewName);

        var update = command.Group.ChangeName(command.NewName);
        if (update.IsFailure)
            return update;

        await repository.Update(update.Value, ct);
        return update;
    }
}

using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.CreateStudentGroup;

public class CreateStudentGroupCommandHandler(IStudentGroupsRepository repository)
    : ICommandHandler<CreateStudentGroupCommand, StudentGroup>
{
    public async Task<Result<StudentGroup>> Handle(
        CreateStudentGroupCommand command,
        CancellationToken ct = default
    )
    {
        if (await repository.HasWithName(command.Name, ct))
            return Result<StudentGroup>.Failure(StudentGroupErrors.NameDublicate(command.Name));

        var group = StudentGroup.Create(command.Name);
        if (group.IsFailure)
            return group;

        await repository.Insert(group.Value, ct);
        return Result<StudentGroup>.Success(group.Value);
    }
}

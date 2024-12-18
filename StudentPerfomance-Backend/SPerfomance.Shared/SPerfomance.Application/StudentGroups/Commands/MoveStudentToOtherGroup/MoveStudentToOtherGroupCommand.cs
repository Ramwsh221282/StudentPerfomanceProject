using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Students.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.MoveStudentToOtherGroup;

public record MoveStudentToOtherGroupCommand(Guid StudentId, Guid CurrentGroupId, Guid OtherGroupId)
    : ICommand<Student>;

public sealed class MoveStudentToOtherGroupCommandHandler(
    IStudentsRepository studentsRepository,
    IStudentGroupsRepository groupsRepository
) : ICommandHandler<MoveStudentToOtherGroupCommand, Student>
{
    public async Task<Result<Student>> Handle(
        MoveStudentToOtherGroupCommand command,
        CancellationToken ct = default
    )
    {
        Result<StudentGroup> currentStudentGroup = await GetStudentCurrentGroup(command, ct);
        if (currentStudentGroup.IsFailure)
            return currentStudentGroup.Error;
        Result<StudentGroup> otherStudentGroup = await GetOtherStudentGroup(command, ct);
        if (otherStudentGroup.IsFailure)
            return otherStudentGroup.Error;
        Result<Student> student = GetStudentFromCurrentGroup(command, currentStudentGroup);
        if (student.IsFailure)
            return student.Error;

        await studentsRepository.ChangeStudentGroupId(student.Value, otherStudentGroup.Value, ct);
        return MoveStudentToOtherGroup(otherStudentGroup.Value, student.Value);
    }

    private async Task<Result<StudentGroup>> GetStudentCurrentGroup(
        MoveStudentToOtherGroupCommand command,
        CancellationToken ct
    )
    {
        StudentGroup? current = await groupsRepository.GetById(command.CurrentGroupId, ct);
        if (current == null)
            return StudentGroupErrors.NotFound();
        return current;
    }

    private async Task<Result<StudentGroup>> GetOtherStudentGroup(
        MoveStudentToOtherGroupCommand command,
        CancellationToken ct
    )
    {
        StudentGroup? other = await groupsRepository.GetById(command.OtherGroupId, ct);
        if (other == null)
            return StudentGroupErrors.NotFound();
        return other;
    }

    private Result<Student> GetStudentFromCurrentGroup(
        MoveStudentToOtherGroupCommand command,
        StudentGroup studentGroup
    ) => studentGroup.GetStudent(s => s.Id == command.StudentId);

    private Result<Student> MoveStudentToOtherGroup(StudentGroup group, Student student) =>
        group.AddStudent(student);
}

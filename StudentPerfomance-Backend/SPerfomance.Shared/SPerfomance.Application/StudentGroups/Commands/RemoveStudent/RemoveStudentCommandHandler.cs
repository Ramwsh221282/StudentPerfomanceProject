using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Students.Abstractions;
using SPerfomance.Domain.Models.Students.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.RemoveStudent;

public class RemoveStudentCommandHandler(IStudentsRepository students)
    : ICommandHandler<RemoveStudentCommand, Student>
{
    public async Task<Result<Student>> Handle(
        RemoveStudentCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Student == null)
            return Result<Student>.Failure(StudentErrors.NotFound());

        var deletion = command.Student.AttachedGroup.RemoveStudent(command.Student);
        if (deletion.IsFailure)
            return deletion;

        await students.Remove(deletion.Value, ct);
        return deletion;
    }
}

using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Students.Abstractions;
using SPerfomance.Domain.Models.Students.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.RemoveStudent;

public class RemoveStudentCommandHandler(IStudentsRepository students)
    : ICommandHandler<RemoveStudentCommand, Student>
{
    private readonly IStudentsRepository _students = students;

    public async Task<Result<Student>> Handle(RemoveStudentCommand command)
    {
        if (command.Student == null)
            return Result<Student>.Failure(StudentErrors.NotFound());

        Result<Student> deletion = command.Student.AttachedGroup.RemoveStudent(command.Student);
        if (deletion.IsFailure)
            return deletion;

        await _students.Remove(deletion.Value);
        return deletion;
    }
}

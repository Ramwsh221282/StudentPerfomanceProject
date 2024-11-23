using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Students.Abstractions;
using SPerfomance.Domain.Models.Students.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.AddStudentCommand;

public class AddStudentCommandHandler(IStudentsRepository students)
    : ICommandHandler<AddStudentCommand, Student>
{
    public async Task<Result<Student>> Handle(
        AddStudentCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Group == null)
            return Result<Student>.Failure(StudentGroupErrors.NotFound());

        if (command.Recordbook == null)
            return new Error("Зачётная книжка не была указана");

        if (await students.HasWithRecordbook(command.Recordbook.Value, ct))
            return StudentErrors.RecordbookDublicate(command.Recordbook.Value);

        var student = command.Group.AddStudent(
            command.Name.ValueOrEmpty(),
            command.Surname.ValueOrEmpty(),
            command.Patronymic.ValueOrEmpty(),
            command.Recordbook.ValueOrEmpty(),
            command.State.ValueOrEmpty()
        );

        if (student.IsFailure)
            return student;

        await students.Insert(student.Value, ct);
        return Result<Student>.Success(student.Value);
    }
}

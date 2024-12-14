using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Errors;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.Abstractions;
using SPerfomance.Domain.Models.Teachers.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Commands.RegisterTeacher;

public class RegisterTeacherCommandHandler(ITeachersRepository repository)
    : ICommandHandler<RegisterTeacherCommand, Teacher>
{
    public async Task<Result<Teacher>> Handle(
        RegisterTeacherCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Department == null)
            return Result<Teacher>.Failure(TeacherDepartmentErrors.NotFound());

        var registeredTeacher = command.Department.RegisterTeacher(
            command.Name,
            command.Surname,
            command.Patronymic,
            command.WorkingState,
            command.JobTitle
        );

        if (registeredTeacher.IsFailure)
            return Result<Teacher>.Failure(registeredTeacher.Error);

        if (
            await repository.TeacherExists(
                registeredTeacher.Value.Name,
                registeredTeacher.Value.JobTitle,
                registeredTeacher.Value.State,
                ct
            )
        )
            return TeacherErrors.TeacherDuplicate();

        await repository.Insert(registeredTeacher.Value, ct);
        return Result<Teacher>.Success(registeredTeacher.Value);
    }
}

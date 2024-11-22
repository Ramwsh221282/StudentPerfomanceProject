using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.Abstractions;
using SPerfomance.Domain.Models.Teachers.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Commands.UpdateTeacher;

public class UpdateTeacherCommandHandler(ITeachersRepository repository)
    : ICommandHandler<UpdateTeacherCommand, Teacher>
{
    public async Task<Result<Teacher>> Handle(
        UpdateTeacherCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Teacher == null)
            return Result<Teacher>.Failure(TeacherErrors.NotFound());

        var updatedTeacher = command.Teacher.ChangeName(
            command.NewName,
            command.NewSurname,
            command.NewPatronymic
        );
        if (updatedTeacher.IsFailure)
            return updatedTeacher;

        updatedTeacher = updatedTeacher.Value.ChangeJobTitle(command.NewJobTitle);
        if (updatedTeacher.IsFailure)
            return updatedTeacher;

        updatedTeacher = updatedTeacher.Value.ChangeState(command.NewState);
        if (updatedTeacher.IsFailure)
            return updatedTeacher;

        await repository.Update(updatedTeacher.Value, ct);
        return Result<Teacher>.Success(updatedTeacher.Value);
    }
}

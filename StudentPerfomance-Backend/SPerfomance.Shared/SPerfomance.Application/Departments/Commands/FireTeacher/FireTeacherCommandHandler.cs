using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.Abstractions;
using SPerfomance.Domain.Models.Teachers.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Commands.FireTeacher;

public class FireTeacherCommandHandler(ITeachersRepository repository)
    : ICommandHandler<FireTeacherCommand, Teacher>
{
    private readonly ITeachersRepository _repository = repository;

    public async Task<Result<Teacher>> Handle(FireTeacherCommand command)
    {
        if (command.Teacher == null)
            return Result<Teacher>.Failure(TeacherErrors.NotFound());

        Result<Teacher> fired = command.Teacher.Department.FireTeacher(command.Teacher);
        if (fired.IsFailure)
            return fired;

        await _repository.Remove(fired.Value);
        return fired;
    }
}

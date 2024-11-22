using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Commands.RemoveTeachersDepartment;

public class RemoveTeachersDepartmentCommandHandler(ITeacherDepartmentsRepository repository)
    : ICommandHandler<RemoveTeachersDepartmentCommand, TeachersDepartments>
{
    public async Task<Result<TeachersDepartments>> Handle(
        RemoveTeachersDepartmentCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Department == null)
            return Result<TeachersDepartments>.Failure(TeacherDepartmentErrors.NotFound());

        await repository.Remove(command.Department, ct);
        return Result<TeachersDepartments>.Success(command.Department);
    }
}

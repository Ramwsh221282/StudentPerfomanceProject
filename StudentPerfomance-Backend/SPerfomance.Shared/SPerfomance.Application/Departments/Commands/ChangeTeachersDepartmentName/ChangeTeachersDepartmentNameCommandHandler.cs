using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Commands.ChangeTeachersDepartmentName;

public class ChangeTeachersDepartmentNameCommandHandler(ITeacherDepartmentsRepository repository)
    : ICommandHandler<ChangeTeachersDepartmentNameCommand, TeachersDepartments>
{
    public async Task<Result<TeachersDepartments>> Handle(
        ChangeTeachersDepartmentNameCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Department == null)
            return Result<TeachersDepartments>.Failure(TeacherDepartmentErrors.NotFound());

        var name = command.NewName.ValueOrEmpty();
        if (await repository.HasWithName(name, ct))
            return Result<TeachersDepartments>.Failure(
                TeacherDepartmentErrors.DepartmentDublicate(name)
            );

        var department = command.Department.ChangeName(command.NewName);
        if (department.IsFailure)
            return department;

        await repository.Update(department.Value, ct);
        return department;
    }
}

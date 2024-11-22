using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Commands.CreateTeachersDepartment;

public class CreateTeachersDepartmentCommandHandler(ITeacherDepartmentsRepository repository)
    : ICommandHandler<CreateTeachersDepartmentCommand, TeachersDepartments>
{
    public async Task<Result<TeachersDepartments>> Handle(
        CreateTeachersDepartmentCommand command,
        CancellationToken ct = default
    )
    {
        var creation = TeachersDepartments.Create(command.Name);
        if (creation.IsFailure)
            return creation;

        if (await repository.HasWithName(command.Name, ct))
            return Result<TeachersDepartments>.Failure(
                TeacherDepartmentErrors.DepartmentDublicate(command.Name)
            );

        await repository.Insert(creation.Value, ct);
        return Result<TeachersDepartments>.Success(creation.Value);
    }
}

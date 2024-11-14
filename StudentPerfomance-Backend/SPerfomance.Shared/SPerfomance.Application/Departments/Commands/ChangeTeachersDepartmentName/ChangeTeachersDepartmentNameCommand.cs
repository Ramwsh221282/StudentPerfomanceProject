using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Commands.ChangeTeachersDepartmentName;

public class ChangeTeachersDepartmentNameCommand(TeachersDepartments? department, string? newName)
    : ICommand<TeachersDepartments>
{
    public TeachersDepartments? Department { get; init; } = department;

    public string NewName { get; init; } = newName.ValueOrEmpty();
}

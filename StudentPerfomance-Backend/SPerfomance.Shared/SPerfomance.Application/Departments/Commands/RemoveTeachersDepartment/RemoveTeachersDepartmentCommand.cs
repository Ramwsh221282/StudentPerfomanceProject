using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments;

namespace SPerfomance.Application.Departments.Commands.RemoveTeachersDepartment;

public class RemoveTeachersDepartmentCommand(TeachersDepartments? department)
    : ICommand<TeachersDepartments>
{
    public TeachersDepartments? Department { get; init; } = department;
}

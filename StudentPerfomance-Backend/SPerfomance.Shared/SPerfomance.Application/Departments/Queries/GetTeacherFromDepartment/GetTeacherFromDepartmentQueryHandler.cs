using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Errors;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Queries.GetTeacherFromDepartment;

public class GetTeacherFromDepartmentQueryHandler
    : IQueryHandler<GetTeacherFromDepartmentQuery, Teacher>
{
    public async Task<Result<Teacher>> Handle(GetTeacherFromDepartmentQuery command)
    {
        if (command.Department == null)
            return Result<Teacher>.Failure(TeacherDepartmentErrors.NotFound());

        Result<Teacher> teacher = command.Department.FindTeacher(
            command.Name,
            command.Surname,
            command.Patronymic,
            command.WorkingState,
            command.JobTitle
        );

        return await Task.FromResult(teacher);
    }
}

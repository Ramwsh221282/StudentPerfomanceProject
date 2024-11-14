using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Queries.GetDepartmentByName;

public class GetDepartmentByNameQueryHandler(ITeacherDepartmentsRepository repository)
    : IQueryHandler<GetDepartmentByNameQuery, TeachersDepartments>
{
    private readonly ITeacherDepartmentsRepository _repository = repository;

    public async Task<Result<TeachersDepartments>> Handle(GetDepartmentByNameQuery command)
    {
        TeachersDepartments? requestedDepartment = await _repository.Get(command.Name);
        return requestedDepartment == null
            ? Result<TeachersDepartments>.Failure(TeacherDepartmentErrors.NotFound())
            : Result<TeachersDepartments>.Success(requestedDepartment);
    }
}

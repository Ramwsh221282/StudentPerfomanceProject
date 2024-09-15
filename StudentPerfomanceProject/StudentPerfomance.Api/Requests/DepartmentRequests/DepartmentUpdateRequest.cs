using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;

namespace StudentPerfomance.Api.Requests.DepartmentRequests;

public record DepartmentUpdateRequest(DepartmentSchema OldDepartment, DepartmentSchema NewDepartment);

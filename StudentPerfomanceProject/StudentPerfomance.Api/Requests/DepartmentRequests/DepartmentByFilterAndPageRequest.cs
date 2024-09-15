using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;

namespace StudentPerfomance.Api.Requests.DepartmentRequests;

public record class DepartmentByFilterAndPageRequest(int Page, int PageSize, DepartmentSchema Department);

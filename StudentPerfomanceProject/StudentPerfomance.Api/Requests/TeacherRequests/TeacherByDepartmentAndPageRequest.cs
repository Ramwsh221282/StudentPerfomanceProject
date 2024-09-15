using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;

namespace StudentPerfomance.Api.Requests.TeacherRequests;

public record TeacherByDepartmentAndPageRequest(int Page, int PageSize, DepartmentSchema Department);

using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;

namespace StudentPerfomance.Api.Requests.TeacherRequests;

public record TeacherCountRequest(DepartmentSchema Department);

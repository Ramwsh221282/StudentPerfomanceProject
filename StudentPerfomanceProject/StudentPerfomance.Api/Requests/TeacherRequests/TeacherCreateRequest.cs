using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;

namespace StudentPerfomance.Api.Requests.TeacherRequests;

public record TeacherCreateRequest(TeacherSchema Teacher, DepartmentSchema Department);

using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;

namespace StudentPerfomance.Api.Requests.TeacherRequests;

public record TeacherByFilterAndPageRequest(int Page, int PageSize, TeacherSchema Teacher, DepartmentSchema Department);

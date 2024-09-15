using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Schemas.Students;

namespace StudentPerfomance.Api.Requests.StudentRequests;

public record StudentFilterAndPageRequest(int Page, int PageSize, StudentSchema Student, StudentsGroupSchema Group);

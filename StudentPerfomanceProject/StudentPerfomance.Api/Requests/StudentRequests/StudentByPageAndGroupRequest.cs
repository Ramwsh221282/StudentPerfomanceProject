using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Api.Requests.StudentRequests;

public record StudentByPageAndGroupRequest(int Page, int PageSize, StudentsGroupSchema Group);

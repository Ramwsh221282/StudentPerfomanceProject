using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Api.Requests.GroupRequests;

public record GroupCreateRequest(StudentsGroupSchema Group);

using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Api.Requests.StudentRequests;

public record StudentCountByGroupRequest(StudentsGroupSchema Group);

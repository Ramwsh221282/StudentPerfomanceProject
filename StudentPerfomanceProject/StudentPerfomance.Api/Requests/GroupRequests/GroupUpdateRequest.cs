using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Api.Requests.GroupRequests;

public record GroupUpdateRequest(StudentsGroupSchema OldGroup, StudentsGroupSchema NewGroup);

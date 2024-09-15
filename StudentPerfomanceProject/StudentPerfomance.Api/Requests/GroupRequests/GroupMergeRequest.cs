using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Api.Requests.GroupRequests;

public record GroupMergeRequest(StudentsGroupSchema TargetGroup, StudentsGroupSchema MergeGroup);

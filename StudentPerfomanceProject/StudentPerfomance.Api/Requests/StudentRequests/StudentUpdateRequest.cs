using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Schemas.Students;

namespace StudentPerfomance.Api.Requests.StudentRequests;

public record StudentUpdateRequest(StudentSchema OldData, StudentSchema NewData, StudentsGroupSchema Group);

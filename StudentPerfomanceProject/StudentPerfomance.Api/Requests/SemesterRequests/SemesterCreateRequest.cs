using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Api.Requests.SemesterRequests;

public record SemesterCreateRequest(SemesterSchema Semester, StudentsGroupSchema Group);

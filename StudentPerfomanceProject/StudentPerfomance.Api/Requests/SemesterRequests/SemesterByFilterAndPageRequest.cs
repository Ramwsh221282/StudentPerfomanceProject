using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Api.Requests.SemesterRequests;

public record SemesterByFilterAndPageRequest(int Page, int PageSize, SemesterSchema Semester, StudentsGroupSchema Group);

using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Api.Requests.SemesterPlanRequests;

public record class SemesterPlanByPageAndGroupSemesterRequest(int Page, int PageSize, SemesterSchema Semester, StudentsGroupSchema Group);

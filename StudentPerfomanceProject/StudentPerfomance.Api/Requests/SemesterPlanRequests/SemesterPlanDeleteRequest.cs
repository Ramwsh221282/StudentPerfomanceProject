using StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;

namespace StudentPerfomance.Api.Requests.SemesterPlanRequests;

public record class SemesterPlanDeleteRequest(SemesterSchema Semester, DisciplineSchema Discipline);

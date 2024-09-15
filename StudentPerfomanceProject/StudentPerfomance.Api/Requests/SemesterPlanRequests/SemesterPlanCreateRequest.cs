using StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Api.Requests.SemesterPlanRequests;

public record SemesterPlanCreateRequest(SemesterSchema Semester, DisciplineSchema Discipline, StudentsGroupSchema Group);

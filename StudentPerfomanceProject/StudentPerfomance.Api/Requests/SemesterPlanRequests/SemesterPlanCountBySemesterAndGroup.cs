using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Api.Requests.SemesterPlanRequests;

public record SemesterPlanCountBySemesterAndGroup(SemesterSchema Semester, StudentsGroupSchema Group);

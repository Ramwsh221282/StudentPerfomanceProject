using StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;

namespace StudentPerfomance.Api.Requests.SemesterPlanRequests;

public record class SemesterPlanSetTeacherRequest(TeacherSchema Teacher, SemesterSchema Semester, DisciplineSchema Discipline);

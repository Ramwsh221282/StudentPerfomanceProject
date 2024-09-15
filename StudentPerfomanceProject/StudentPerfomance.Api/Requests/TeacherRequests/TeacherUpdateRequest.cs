using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;

namespace StudentPerfomance.Api.Requests.TeacherRequests;

public record TeacherUpdateRequest(TeacherSchema OldTeacher, TeacherSchema NewTeacher);

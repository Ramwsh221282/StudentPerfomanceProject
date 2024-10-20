using SPerfomance.Application.Shared.Module.DTOs.Teachers;

namespace SPerfomance.Application.Teachers.Module.Api.Requests;

public record UpdateTeacherRequest(TeacherDTO? Initial, TeacherDTO? Updated, string Token);

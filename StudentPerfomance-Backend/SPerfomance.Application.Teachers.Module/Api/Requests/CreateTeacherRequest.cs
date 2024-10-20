using SPerfomance.Application.Shared.Module.DTOs.Teachers;

namespace SPerfomance.Application.Teachers.Module.Api.Requests;

public record CreateTeacherRequest(TeacherDTO? Teacher, string Token);

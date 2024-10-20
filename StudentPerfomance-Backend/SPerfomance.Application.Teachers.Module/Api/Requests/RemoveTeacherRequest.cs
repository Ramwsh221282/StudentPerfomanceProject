using SPerfomance.Application.Shared.Module.DTOs.Teachers;

namespace SPerfomance.Application.Teachers.Module.Api.Requests;

public record RemoveTeacherRequest(TeacherDTO? Teacher, string Token);

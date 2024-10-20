using SPerfomance.Application.Shared.Module.DTOs.Students;

namespace SPerfomance.Application.Students.Module.Api.Request;

public record StudentFilterRequest(StudentDTO Student, int Page, int PageSize, string Token);

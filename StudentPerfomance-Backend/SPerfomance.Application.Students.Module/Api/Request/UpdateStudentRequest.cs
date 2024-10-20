using SPerfomance.Application.Shared.Module.DTOs.Students;

namespace SPerfomance.Application.Students.Module.Api.Request;

public record UpdateStudentRequest(StudentDTO Initial, StudentDTO Updated, string Token);

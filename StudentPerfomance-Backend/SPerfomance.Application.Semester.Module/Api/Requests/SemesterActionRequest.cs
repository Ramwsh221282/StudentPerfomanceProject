using SPerfomance.Application.Shared.Module.DTOs.Semesters;

namespace SPerfomance.Application.Semester.Module.Api.Requests;

public record SemesterActionRequest(SemesterDTO Semester, string Token);

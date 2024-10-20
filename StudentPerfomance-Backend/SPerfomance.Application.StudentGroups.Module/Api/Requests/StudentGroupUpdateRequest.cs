using SPerfomance.Application.Shared.Module.DTOs.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Api.Requests;

public record StudentGroupUpdateRequest(StudentGroupDTO Initial, StudentGroupDTO Updated, string Token);

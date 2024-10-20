using SPerfomance.Application.Shared.Module.DTOs.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Api.Requests;

public record StudentGroupSearchRequest(StudentGroupDTO Group, string Token);

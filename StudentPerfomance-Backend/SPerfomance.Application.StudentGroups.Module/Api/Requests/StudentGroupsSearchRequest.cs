using SPerfomance.Application.Shared.Module.DTOs.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Api.Requests;

public record StudentGroupsSearchRequest(StudentGroupDTO Group, string Token);

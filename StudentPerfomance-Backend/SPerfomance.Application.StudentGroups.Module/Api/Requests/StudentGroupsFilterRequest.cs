using SPerfomance.Application.Shared.Module.DTOs.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Api.Requests;

public record StudentGroupsFilterRequest(StudentGroupDTO Group, int Page, int PageSize);

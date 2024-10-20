using SPerfomance.Application.Shared.Module.DTOs.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Api.Requests;

public record StudentGroupMergeRequest(StudentGroupDTO? initial, StudentGroupDTO? target, string Token);

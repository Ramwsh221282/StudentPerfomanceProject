namespace SPerfomance.Application.StudentGroups.Module.Api.Requests;

public record StudentGroupPagedDataRequest(int Page, int PageSize, string Token);

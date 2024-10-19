namespace SPerfomance.Application.Shared.Users.Module.API.Requests;

public record GetPagedUsersRequest(int Page, int PageSize, string Token);

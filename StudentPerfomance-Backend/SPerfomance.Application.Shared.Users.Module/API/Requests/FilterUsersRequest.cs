namespace SPerfomance.Application.Shared.Users.Module.API.Requests;

public record FilterUsersRequest(string? Name, string? Surname, string? Thirdname, string? Email, int Page, int PageSize, string token);

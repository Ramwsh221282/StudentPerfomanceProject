namespace SPerfomance.Application.Students.Module.Api.Request;

public record class StudentPagedDataRequest(int Page, int PageSize, string Token);

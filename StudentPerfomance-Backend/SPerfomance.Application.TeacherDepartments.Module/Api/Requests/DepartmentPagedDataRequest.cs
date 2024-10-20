namespace SPerfomance.Application.TeacherDepartments.Module.Api.Requests;

public record DepartmentPagedDataRequest(int Page, int PageSize, string Token);

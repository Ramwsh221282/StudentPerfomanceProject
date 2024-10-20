using SPerfomance.Application.Shared.Module.DTOs.Departments;

namespace SPerfomance.Application.TeacherDepartments.Module.Api.Requests;

public record DepartmentSearchDataRequest(DepartmentDTO Department, string Token);

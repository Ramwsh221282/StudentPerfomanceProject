using SPerfomance.Application.Shared.Module.DTOs.Departments;

namespace SPerfomance.Application.TeacherDepartments.Module.Api.Requests;

public record DepartmentFilterRequest(DepartmentDTO? Department, int Page, int PageSize);

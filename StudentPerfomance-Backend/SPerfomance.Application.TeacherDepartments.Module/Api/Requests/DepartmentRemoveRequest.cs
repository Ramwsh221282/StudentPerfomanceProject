using SPerfomance.Application.Shared.Module.DTOs.Departments;

namespace SPerfomance.Application.TeacherDepartments.Module.Api.Requests;

public record DepartmentRemoveRequest(DepartmentDTO? Department, string Token);

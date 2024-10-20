using SPerfomance.Application.Shared.Module.DTOs.Departments;

namespace SPerfomance.Application.Teachers.Module.Api.Requests;

public record GetDepartmentTeachersRequest(DepartmentDTO? Department, string Token);

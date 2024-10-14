using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.DTOs.Departments;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.TeacherDepartments.Module.Api.Requests;
using SPerfomance.Application.TeacherDepartments.Module.Queries.All;
using SPerfomance.Application.TeacherDepartments.Module.Queries.Count;
using SPerfomance.Application.TeacherDepartments.Module.Queries.Filtered;
using SPerfomance.Application.TeacherDepartments.Module.Queries.Paged;
using SPerfomance.Application.TeacherDepartments.Module.Queries.Searched;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Api;

[ApiController]
[Route("/teacher-departments/api/read")]
public sealed class TeacherDepartmentsReadApi
{
	[HttpGet(CrudOperationNames.GetCount)]
	public async Task<ActionResult<int>> GetCount()
	{
		GetTeachersDepartmentsCountQuery query = new GetTeachersDepartmentsCountQuery();
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result.Result);
	}

	[HttpGet(CrudOperationNames.GetAll)]
	public async Task<ActionResult<IReadOnlyCollection<DepartmentSchema>>> GetAllDepartments()
	{
		GetAllTeacherDepartmentsQuery query = new GetAllTeacherDepartmentsQuery();
		OperationResult<IReadOnlyCollection<TeachersDepartment>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.GetPaged)]
	public async Task<ActionResult<IReadOnlyCollection<DepartmentSchema>>> GetPagedDepartments(int page, int pageSize)
	{
		GetTeachersDepartmentPagedQuery query = new GetTeachersDepartmentPagedQuery(page, pageSize);
		OperationResult<IReadOnlyCollection<TeachersDepartment>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<DepartmentSchema>>> Search([FromQuery] DepartmentSchema department)
	{
		SearchTeachersDepartmentsQuery query = new SearchTeachersDepartmentsQuery(department);
		OperationResult<IReadOnlyCollection<TeachersDepartment>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Filter)]
	public async Task<ActionResult<IReadOnlyCollection<DepartmentSchema>>> GetFiltered([FromQuery] DepartmentFilterRequest request)
	{
		int page = request.Page;
		int pageSize = request.PageSize;
		DepartmentSchema department = request.Department.ToSchema();

		FilterTeacherDepartmentsQuery query = new FilterTeacherDepartmentsQuery(department, page, pageSize);
		OperationResult<IReadOnlyCollection<TeachersDepartment>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}

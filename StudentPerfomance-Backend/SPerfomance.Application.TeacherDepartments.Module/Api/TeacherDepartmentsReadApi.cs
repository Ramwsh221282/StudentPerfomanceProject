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
	public async Task<ActionResult<int>> GetCount([FromQuery] DepartmentDataRequest request)
	{
		string token = request.Token;
		GetTeachersDepartmentsCountQuery query = new GetTeachersDepartmentsCountQuery(token);
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result.Result);
	}

	[HttpGet(CrudOperationNames.GetAll)]
	public async Task<ActionResult<IReadOnlyCollection<DepartmentSchema>>> GetAllDepartments([FromQuery] DepartmentDataRequest request)
	{
		string token = request.Token;
		GetAllTeacherDepartmentsQuery query = new GetAllTeacherDepartmentsQuery(token);
		OperationResult<IReadOnlyCollection<TeachersDepartment>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.GetPaged)]
	public async Task<ActionResult<IReadOnlyCollection<DepartmentSchema>>> GetPagedDepartments([FromQuery] DepartmentPagedDataRequest request)
	{
		int page = request.Page;
		int pageSize = request.PageSize;
		string token = request.Token;
		GetTeachersDepartmentPagedQuery query = new GetTeachersDepartmentPagedQuery(page, pageSize, token);
		OperationResult<IReadOnlyCollection<TeachersDepartment>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<DepartmentSchema>>> Search([FromQuery] DepartmentSearchDataRequest request)
	{
		DepartmentSchema department = request.Department.ToSchema();
		string token = request.Token;
		SearchTeachersDepartmentsQuery query = new SearchTeachersDepartmentsQuery(department, token);
		OperationResult<IReadOnlyCollection<TeachersDepartment>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Filter)]
	public async Task<ActionResult<IReadOnlyCollection<DepartmentSchema>>> GetFiltered([FromQuery] DepartmentFilterRequest request)
	{
		int page = request.Page;
		int pageSize = request.PageSize;
		DepartmentSchema department = request.Department.ToSchema();
		string token = request.Token;
		FilterTeacherDepartmentsQuery query = new FilterTeacherDepartmentsQuery(department, page, pageSize, token);
		OperationResult<IReadOnlyCollection<TeachersDepartment>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}

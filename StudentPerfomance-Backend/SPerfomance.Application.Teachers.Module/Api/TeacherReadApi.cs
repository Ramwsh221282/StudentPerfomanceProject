using Microsoft.AspNetCore.Mvc;
using SPerfomance.Application.Shared.Module.DTOs.Departments;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Api.Requests;
using SPerfomance.Application.Teachers.Module.Queries.All;
using SPerfomance.Application.Teachers.Module.Queries.Count;
using SPerfomance.Application.Teachers.Module.Queries.Filtered;
using SPerfomance.Application.Teachers.Module.Queries.GetDepartmentTeachers;
using SPerfomance.Application.Teachers.Module.Queries.Paged;
using SPerfomance.Application.Teachers.Module.Queries.Searched;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Api;

[ApiController]
[Route("/teacher/api/read")]
public sealed class TeacherReadApi : Controller
{
	[HttpGet(CrudOperationNames.GetAll)]
	public async Task<ActionResult<IReadOnlyCollection<TeacherSchema>>> GetAll()
	{
		GetAllQuery query = new GetAllQuery();
		OperationResult<IReadOnlyCollection<Teacher>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.GetCount)]
	public async Task<ActionResult<int>> GetCount()
	{
		GetCountQuery query = new GetCountQuery();
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result);
	}

	[HttpGet(CrudOperationNames.GetPaged)]
	public async Task<ActionResult<IReadOnlyCollection<TeacherSchema>>> GetPaged(int page, int pageSize)
	{
		GetPagedQuery query = new GetPagedQuery(page, pageSize);
		OperationResult<IReadOnlyCollection<Teacher>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Filter)]
	public async Task<ActionResult<IReadOnlyCollection<TeacherSchema>>> Filter([FromQuery] TeacherSchema teacher, int page, int pageSize)
	{
		FilterQuery query = new FilterQuery(teacher, page, pageSize);
		OperationResult<IReadOnlyCollection<Teacher>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<TeacherSchema>>> Search([FromQuery] TeacherSchema teacher)
	{
		SearchQuery query = new SearchQuery(teacher);
		OperationResult<IReadOnlyCollection<Teacher>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet("department-teachers")]
	public async Task<ActionResult<IReadOnlyCollection<TeacherSchema>>> GetDepartmentTeacher([FromQuery] GetDepartmentTeachersRequest request)
	{
		if (request == null)
			return new BadRequestObjectResult(new DepartmentNotFountError().ToString());

		DepartmentSchema department = request.Department.ToSchema();
		GetDepartmentTeachersQuery query = new GetDepartmentTeachersQuery(department);
		OperationResult<IReadOnlyCollection<Teacher>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}

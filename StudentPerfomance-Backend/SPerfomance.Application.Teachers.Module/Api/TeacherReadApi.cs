using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.DTOs.Departments;
using SPerfomance.Application.Shared.Module.DTOs.Teachers;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Api.Requests;
using SPerfomance.Application.Teachers.Module.Queries.GetDepartmentTeachers;
using SPerfomance.Application.Teachers.Module.Queries.Searched;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Api;

[ApiController]
[Route("/teacher/api/read")]
public sealed class TeacherReadApi : Controller
{
	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<TeacherSchema>>> Search([FromQuery] FilterTeacherRequest request)
	{
		string token = request.Token;
		TeacherSchema teacher = request.Teacher.ToSchema();
		SearchQuery query = new SearchQuery(teacher, token);
		OperationResult<IReadOnlyCollection<Teacher>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet("department-teachers")]
	public async Task<ActionResult<IReadOnlyCollection<TeacherSchema>>> GetDepartmentTeacher([FromQuery] GetDepartmentTeachersRequest request)
	{
		if (request == null)
			return new BadRequestObjectResult(new DepartmentNotFountError().ToString());

		DepartmentSchema department = request.Department.ToSchema();
		string token = request.Token;
		GetDepartmentTeachersQuery query = new GetDepartmentTeachersQuery(department, token);
		OperationResult<IReadOnlyCollection<Teacher>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}

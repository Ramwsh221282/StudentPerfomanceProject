using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Facades.TeacherDepartments;
using SPerfomance.Application.Shared.Module.Schemas.Departments;

using Response = Microsoft.AspNetCore.Mvc.ActionResult<SPerfomance.Api.Module.Responses.TeacherDepartments.TeacherDepartmentResponse>;
using CollectionResponse = Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IReadOnlyCollection<SPerfomance.Api.Module.Responses.TeacherDepartments.TeacherDepartmentResponse>>;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TeacherDepartmentsController : Controller
{
	[HttpGet("all")]
	public async Task<CollectionResponse> GetAll()
	{
		TeacherDepartmentGetAllFacade facade = new TeacherDepartmentGetAllFacade();
		return await facade.Process();
	}

	[HttpGet("count")]
	public async Task<ActionResult<int>> Count()
	{
		TeacherDepartmentCountFacade facade = new TeacherDepartmentCountFacade();
		return await facade.Process();
	}

	[HttpGet("filter")]
	public async Task<CollectionResponse> Filter([FromQuery] DepartmentSchema schema, int page, int pageSize)
	{
		TeacherDepartmentFilterFacade facade = new TeacherDepartmentFilterFacade(page, pageSize, schema);
		return await facade.Process();
	}

	[HttpGet("search")]
	public async Task<CollectionResponse> Search([FromQuery] DepartmentSchema schema)
	{
		TeacherDepartmentSearchFacade facade = new TeacherDepartmentSearchFacade(schema);
		return await facade.Process();
	}

	[HttpGet("byPage")]
	public async Task<CollectionResponse> GetPaged(int page, int pageSize)
	{
		TeacherDepartmentPaginationFacade facade = new TeacherDepartmentPaginationFacade(page, pageSize);
		return await facade.Process();
	}

	[HttpPost]
	public async Task<Response> Create([FromBody] DepartmentSchema schema)
	{
		TeacherDepartmentCreateFacade facade = new TeacherDepartmentCreateFacade(schema);
		return await facade.Process();
	}

	[HttpPut]
	public async Task<Response> Update([FromQuery] DepartmentSchema oldSchema, [FromQuery] DepartmentSchema newSchema)
	{
		TeacherDepartmentUpdateFacade facade = new TeacherDepartmentUpdateFacade(oldSchema, newSchema);
		return await facade.Process();
	}

	[HttpDelete]
	public async Task<Response> Delete([FromBody] DepartmentSchema schema)
	{
		TeacherDepartmentDeleteFacade facade = new TeacherDepartmentDeleteFacade(schema);
		return await facade.Process();
	}
}

using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Facades.StudentGroups;
using SPerfomance.Api.Module.Responses.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class StudentGroupsController : Controller
{
	[HttpGet("count")]
	public async Task<ActionResult<int>> GetCount() => await new StudentGroupCountFacade().Process();
	[HttpGet("all")]
	public async Task<ActionResult<IReadOnlyCollection<StudentGroupResponse>>> GetAll() => await new StudentGroupGetAllFacade().Process();
	[HttpGet("filter")]
	public async Task<ActionResult<IReadOnlyCollection<StudentGroupResponse>>> GetByFilter([FromQuery] StudentsGroupSchema schema, int page, int pageSize)
	{
		StudentGroupPagedFilteredFacade facade = new StudentGroupPagedFilteredFacade(page, pageSize, schema);
		return await facade.Process();
	}
	[HttpGet("search")]
	public async Task<ActionResult<IReadOnlyCollection<StudentGroupResponse>>> GetBySearch([FromQuery] StudentsGroupSchema schema)
	{
		StudentGroupSearchFacade facade = new StudentGroupSearchFacade(schema);
		return await facade.Process();
	}
	[HttpGet("paged")]
	public async Task<ActionResult<IReadOnlyCollection<StudentGroupResponse>>> GetPaged(int page, int pageSize)
	{
		StudentGroupPagedFacade facade = new StudentGroupPagedFacade(page, pageSize);
		return await facade.Process();
	}
	[HttpPost]
	public async Task<ActionResult<StudentGroupResponse>> Create([FromBody] StudentsGroupSchema schema)
	{
		StudentGroupCreationFacade facade = new StudentGroupCreationFacade(schema);
		return await facade.Process();
	}
	[HttpDelete]
	public async Task<ActionResult<StudentGroupResponse>> Delete([FromBody] StudentsGroupSchema schema)
	{
		StudentGroupDeletionFacade facade = new StudentGroupDeletionFacade(schema);
		return await facade.Process();
	}

	[HttpPut]
	public async Task<ActionResult<StudentGroupResponse>> Update([FromQuery] StudentsGroupSchema oldSchema, [FromQuery] StudentsGroupSchema newSchema)
	{
		StudentGroupUpdateFacade facade = new StudentGroupUpdateFacade(oldSchema, newSchema);
		return await facade.Process();
	}
}

using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Facades.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;

using Response = Microsoft.AspNetCore.Mvc.ActionResult<SPerfomance.Api.Module.Responses.StudentGroups.StudentGroupResponse>;
using CollectionResponse = Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IReadOnlyCollection<SPerfomance.Api.Module.Responses.StudentGroups.StudentGroupResponse>>;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class StudentGroupsController : Controller
{
	[HttpGet("count")]
	public async Task<ActionResult<int>> GetCount() => await new StudentGroupCountFacade().Process();
	[HttpGet("all")]
	public async Task<CollectionResponse> GetAll() => await new StudentGroupGetAllFacade().Process();
	[HttpGet("filter")]
	public async Task<CollectionResponse> GetByFilter([FromQuery] StudentsGroupSchema schema, int page, int pageSize)
	{
		StudentGroupPagedFilteredFacade facade = new StudentGroupPagedFilteredFacade(page, pageSize, schema);
		return await facade.Process();
	}
	[HttpGet("search")]
	public async Task<CollectionResponse> GetBySearch([FromQuery] StudentsGroupSchema schema)
	{
		StudentGroupSearchFacade facade = new StudentGroupSearchFacade(schema);
		return await facade.Process();
	}
	[HttpGet("paged")]
	public async Task<CollectionResponse> GetPaged(int page, int pageSize)
	{
		StudentGroupPagedFacade facade = new StudentGroupPagedFacade(page, pageSize);
		return await facade.Process();
	}
	[HttpPost]
	public async Task<Response> Create([FromBody] StudentsGroupSchema schema)
	{
		StudentGroupCreationFacade facade = new StudentGroupCreationFacade(schema);
		return await facade.Process();
	}
	[HttpDelete]
	public async Task<Response> Delete([FromBody] StudentsGroupSchema schema)
	{
		StudentGroupDeletionFacade facade = new StudentGroupDeletionFacade(schema);
		return await facade.Process();
	}

	[HttpPut]
	public async Task<Response> Update([FromQuery] StudentsGroupSchema oldSchema, [FromQuery] StudentsGroupSchema newSchema)
	{
		StudentGroupUpdateFacade facade = new StudentGroupUpdateFacade(oldSchema, newSchema);
		return await facade.Process();
	}
}

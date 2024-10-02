using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Api.Module.Facades.Teachers;

using Response = Microsoft.AspNetCore.Mvc.ActionResult<SPerfomance.Api.Module.Responses.Teachers.TeacherResponse>;
using CollectionResponse = Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IReadOnlyCollection<SPerfomance.Api.Module.Responses.Teachers.TeacherResponse>>;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TeachersController : Controller
{
	[HttpPost]
	public async Task<Response> Create([FromBody] TeacherSchema teacher)
	{
		TeacherCreationFacade facade = new TeacherCreationFacade(teacher);
		return await facade.Process();
	}

	[HttpDelete]
	public async Task<Response> Delete([FromBody] TeacherSchema teacher)
	{
		TeacherDeletionFacade facade = new TeacherDeletionFacade(teacher);
		return await facade.Process();
	}

	[HttpPut]
	public async Task<Response> Update([FromQuery] TeacherSchema oldSchema, [FromQuery] TeacherSchema newSchema)
	{
		TeacherUpdateFacade facade = new TeacherUpdateFacade(oldSchema, newSchema);
		return await facade.Process();
	}

	[HttpGet("byPage")]
	public async Task<CollectionResponse> GetPaged(int page, int pageSize)
	{
		TeacherPaginationFacade facade = new TeacherPaginationFacade(page, pageSize);
		return await facade.Process();
	}

	[HttpGet("filter")]
	public async Task<CollectionResponse> GetFiltered([FromQuery] TeacherSchema schema, int page, int pageSize)
	{
		TeacherFilterFacade facade = new TeacherFilterFacade(page, pageSize, schema);
		return await facade.Process();
	}

	[HttpGet("search")]
	public async Task<CollectionResponse> Search([FromQuery] TeacherSchema schema)
	{
		TeacherSearchFacade facade = new TeacherSearchFacade(schema);
		return await facade.Process();
	}

	[HttpGet("count")]
	public async Task<ActionResult<int>> Count()
	{
		TeacherCountFacade facade = new TeacherCountFacade();
		return await facade.Process();
	}
}

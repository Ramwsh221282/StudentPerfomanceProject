using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Facades.Semesters;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;

using CollectionResponse = Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IReadOnlyCollection<SPerfomance.Api.Module.Responses.Semesters.SemesterResponse>>;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public class SemestersController : Controller
{
	[HttpGet("byPage")]
	public async Task<CollectionResponse> GetByPage(int page, int pageSize) =>
		 await new SemestersPaginationFacade(page, pageSize).Process();

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> TotalCount() =>
		await new SemestersCountFacade().Process();

	[HttpGet("byFilter")]
	public async Task<CollectionResponse> GetPagedByFilter([FromQuery] SemesterSchema semester, int page, int pageSize) =>
		await new SemestersPaginationFilteredFacade(semester, page, pageSize).Process();

	[HttpGet("search")]
	public async Task<CollectionResponse> GetBySearch([FromQuery] SemesterSchema semester) =>
		await new SemestersSearchFacade(semester).Process();
}

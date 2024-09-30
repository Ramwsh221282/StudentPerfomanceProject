using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Facades.Semesters;
using SPerfomance.Api.Module.Responses.Semesters;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public class SemestersController : Controller
{
	[HttpGet("byPage")]
	public async Task<ActionResult<IReadOnlyCollection<SemesterResponse>>> GetByPage(int page, int pageSize) =>
		 await new SemestersPaginationFacade(page, pageSize).Process();

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> TotalCount() =>
		await new SemestersCountFacade().Process();

	[HttpGet("byFilter")]
	public async Task<ActionResult<IReadOnlyCollection<SemesterResponse>>> GetPagedByFilter([FromQuery] SemesterSchema semester, int page, int pageSize) =>
		await new SemestersPaginationFilteredFacade(semester, page, pageSize).Process();

	[HttpGet("search")]
	public async Task<ActionResult<IReadOnlyCollection<SemesterResponse>>> GetBySearch([FromQuery] SemesterSchema semester) =>
		await new SemestersSearchFacade(semester).Process();
}

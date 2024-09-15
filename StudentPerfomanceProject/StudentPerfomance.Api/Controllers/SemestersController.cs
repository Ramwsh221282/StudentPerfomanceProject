using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.SemesterRequests;
using StudentPerfomance.Api.Responses.Semesters;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Semesters.CreateSemester;
using StudentPerfomance.Application.Commands.Semesters.DeleteSemester;
using StudentPerfomance.Application.Queries.Semesters.GetSemestersByFilter;
using StudentPerfomance.Application.Queries.Semesters.GetSemestersByPage;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class SemestersController : Controller
{
	private readonly IRepository<Semester> _semesters = new SemestersRepository();
	private readonly IRepository<StudentGroup> _groups = new StudentGroupsRepository();

	[HttpGet("byPage")]
	public async Task<ActionResult<IReadOnlyCollection<SemesterResponse>>> GetSemestersByPage([FromQuery] SemesterByPageRequest request)
	{
		IService<IReadOnlyCollection<Semester>> service = new SemestersPaginationService(request.Page, request.PageSize, _semesters);
		return SemesterResponse.FromResult(await service.DoOperation());
	}

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> GetSemestersAmount()
	{
		int count = await _semesters.Count();
		return Ok(count);
	}

	[HttpGet("byFilter")]
	public async Task<ActionResult<IReadOnlyCollection<SemesterResponse>>> GetSemestersByFilter([FromQuery] SemesterByFilterAndPageRequest request)
	{
		SemestersRepositoryParameter semesterParameter = SemesterSchemaConverter.ToRepositoryParameter(request.Semester);
		StudentGroupsRepositoryParameter groupParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IService<IReadOnlyCollection<Semester>> service = new SemesterFilterService
		(
			request.Page,
			request.PageSize,
			SemesterExpressionFactory.CreateFilter(semesterParameter, groupParameter),
			_semesters
		);
		return SemesterResponse.FromResult(await service.DoOperation());
	}

	[HttpPost]
	public async Task<ActionResult<SemesterResponse>> CreateSemester([FromBody] SemesterCreateRequest request)
	{
		SemestersRepositoryParameter semesterParameter = SemesterSchemaConverter.ToRepositoryParameter(request.Semester);
		StudentGroupsRepositoryParameter groupParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IService<Semester> service = new SemestersCreationService
		(
			request.Semester,
			SemesterExpressionFactory.CreateHasSemesterExpression(semesterParameter, groupParameter),
			StudentGroupsExpressionFactory.CreateHasGroupExpression(groupParameter),
			_semesters,
			_groups
		);
		return SemesterResponse.FromResult(await service.DoOperation());
	}

	[HttpDelete]
	public async Task<ActionResult<SemesterResponse>> DeleteSemester([FromBody] SemesterDeleteRequest request)
	{
		SemestersRepositoryParameter semesterParameter = SemesterSchemaConverter.ToRepositoryParameter(request.Semester);
		StudentGroupsRepositoryParameter groupParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IService<Semester> service = new SemesterDeletionService
		(
			SemesterExpressionFactory.CreateHasSemesterExpression(semesterParameter, groupParameter),
			_semesters
		);
		return SemesterResponse.FromResult(await service.DoOperation());
	}
}

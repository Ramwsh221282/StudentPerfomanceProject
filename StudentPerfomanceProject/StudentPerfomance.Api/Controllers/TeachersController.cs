using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.TeacherRequests;
using StudentPerfomance.Api.Responses.Teachers;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Teachers.ChangeTeacherData;
using StudentPerfomance.Application.Commands.Teachers.CreateTeacher;
using StudentPerfomance.Application.Commands.Teachers.DeleteTeacher;
using StudentPerfomance.Application.Queries.Teachers.GetTeachersByDepartment;
using StudentPerfomance.Application.Queries.Teachers.GetTeachersByFilter;
using StudentPerfomance.Application.Queries.Teachers.GetTeachersByPage;
using StudentPerfomance.Application.Queries.Teachers.GetTeachersCountByDepartment;
using StudentPerfomance.DataAccess.Repositories.Teachers;
using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api;

[ApiController]
[Route("[controller]")]
public sealed class TeachersController : Controller
{
	private readonly IRepository<Teacher> _teachersRepository = new TeachersRepository();
	private readonly IRepository<TeachersDepartment> _departmentsRepository = new TeacherDepartmentsRepository();

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> GetTotalAmount([FromQuery] TeacherCountRequest request)
	{
		TeachersDepartmentRepositoryParameter parameter = DepartmentSchemaConverter.ToRepositoryParameter(request.Department);
		IService<int> service = new TeachersCountByDepartmentService(TeacherExpressionFactory.CreateByDepartment(parameter), _teachersRepository);
		OperationResult<int> result = await service.DoOperation();
		return Ok(result.Result);
	}

	[HttpGet("byPage")]
	public async Task<ActionResult<IReadOnlyCollection<TeacherResponse>>> GetTeachersByPage([FromQuery] TeacherByDepartmentAndPageRequest request)
	{
		TeachersDepartmentRepositoryParameter parameter = DepartmentSchemaConverter.ToRepositoryParameter(request.Department);
		IService<IReadOnlyCollection<Teacher>> service = new TeachersPaginationService
		(
			request.Page,
			request.PageSize,
			TeacherExpressionFactory.CreateByDepartment(parameter),
			_teachersRepository
		);
		return TeacherResponse.FromResult(await service.DoOperation());
	}

	[HttpGet("byFilter")]
	public async Task<ActionResult<IReadOnlyCollection<TeacherResponse>>> GetTeachersByFilterAndPage([FromQuery] TeacherByFilterAndPageRequest request)
	{
		TeacherRepositoryParameter teacherParameter = TeacherSchemaConverter.ToRepositoryParameter(request.Teacher);
		TeachersDepartmentRepositoryParameter departmentParameter = DepartmentSchemaConverter.ToRepositoryParameter(request.Department);
		IService<IReadOnlyCollection<Teacher>> service = new TeachersFilterService
		(
			request.Page,
			request.PageSize,
			TeacherExpressionFactory.CreateFilter(teacherParameter, departmentParameter),
			_teachersRepository
		);
		return TeacherResponse.FromResult(await service.DoOperation());
	}

	[HttpGet("byDepartment")]
	public async Task<ActionResult<IReadOnlyCollection<TeacherResponse>>> GetTeachersByDepartment([FromQuery] TeacherByDepartmentRequest request)
	{
		TeachersDepartmentRepositoryParameter parameter = DepartmentSchemaConverter.ToRepositoryParameter(request.Department);
		IService<IReadOnlyCollection<Teacher>> service = new TeachersByDepartmentService
		(
			TeacherExpressionFactory.CreateByDepartment(parameter),
			_teachersRepository
		);
		return TeacherResponse.FromResult(await service.DoOperation());
	}

	[HttpPost]
	public async Task<ActionResult<TeacherResponse>> CreateTeacher([FromBody] TeacherCreateRequest request)
	{
		TeacherRepositoryParameter teacherParameter = TeacherSchemaConverter.ToRepositoryParameter(request.Teacher);
		TeachersDepartmentRepositoryParameter departmentParameter = DepartmentSchemaConverter.ToRepositoryParameter(request.Department);
		IService<Teacher> service = new TeacherCreationService
		(
			request.Teacher,
			TeacherExpressionFactory.CreateByName(teacherParameter),
			TeacherDepartmentsExpressionFactory.CreateHasDepartmentExpression(departmentParameter),
			_teachersRepository,
			_departmentsRepository
		);
		return TeacherResponse.FromResult(await service.DoOperation());
	}

	[HttpDelete]
	public async Task<ActionResult<TeacherResponse>> DeleteTeacher([FromBody] TeacherDeleteRequest request)
	{
		TeacherRepositoryParameter parameter = TeacherSchemaConverter.ToRepositoryParameter(request.Teacher);
		IService<Teacher> service = new TeachersDeletionService
		(
			request.Teacher,
			_teachersRepository,
			TeacherExpressionFactory.CreateByName(parameter)
		);
		return TeacherResponse.FromResult(await service.DoOperation());
	}

	[HttpPut]
	public async Task<ActionResult<TeacherResponse>> ChangeTeacherData([FromBody] TeacherUpdateRequest request)
	{
		TeacherRepositoryParameter oldParameter = TeacherSchemaConverter.ToRepositoryParameter(request.OldTeacher);
		TeacherRepositoryParameter newParameter = TeacherSchemaConverter.ToRepositoryParameter(request.NewTeacher);
		IService<Teacher> service = new TeacherDataChangeService
		(
			request.NewTeacher,
			TeacherExpressionFactory.CreateByName(oldParameter),
			TeacherExpressionFactory.CreateByName(newParameter),
			_teachersRepository
		);
		return TeacherResponse.FromResult(await service.DoOperation());
	}
}

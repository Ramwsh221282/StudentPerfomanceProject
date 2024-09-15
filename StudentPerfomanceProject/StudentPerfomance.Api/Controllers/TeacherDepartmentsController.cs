using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.DepartmentRequests;
using StudentPerfomance.Api.Responses.TeacherDepartments;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Departments.ChangeDepartmentName;
using StudentPerfomance.Application.Commands.Departments.CreateDepartment;
using StudentPerfomance.Application.Commands.Departments.DeleteDepartment;
using StudentPerfomance.Application.Queries.Departments.GetDepartmentsByFilter;
using StudentPerfomance.Application.Queries.Departments.GetDepartmentsByFilterAndPage;
using StudentPerfomance.Application.Queries.Departments.GetDepartmentsByPage;
using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TeacherDepartmentsController : Controller
{
	private readonly IRepository<TeachersDepartment> _repository = new TeacherDepartmentsRepository();

	[HttpGet("all")]
	public async Task<ActionResult<IReadOnlyCollection<TeacherDepartmentResponse>>> GetAll()
	{
		IReadOnlyCollection<TeachersDepartment> teachers = await _repository.GetAll();
		OperationResult<IReadOnlyCollection<TeachersDepartment>> result = new OperationResult<IReadOnlyCollection<TeachersDepartment>>(teachers);
		return TeacherDepartmentResponse.FromResult(result);
	}

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> GetTotalDepartmentsAmount()
	{
		int count = await _repository.Count();
		return Ok(count);
	}

	[HttpGet("byPage")]
	public async Task<ActionResult<IReadOnlyCollection<TeacherDepartmentResponse>>> GetDepartmentsByPage([FromQuery] DepartmentByPageRequest request)
	{
		IService<IReadOnlyCollection<TeachersDepartment>> service = new DepartmentsPaginationService(request.Page, request.PageSize, _repository);
		return TeacherDepartmentResponse.FromResult(await service.DoOperation());
	}

	[HttpGet("byFilter")]
	public async Task<ActionResult<IReadOnlyCollection<TeacherDepartmentResponse>>> GetDepartmentsByFilterAndPage([FromQuery] DepartmentByFilterAndPageRequest request)
	{
		TeachersDepartmentRepositoryParameter parameter = DepartmentSchemaConverter.ToRepositoryParameter(request.Department);
		IService<IReadOnlyCollection<TeachersDepartment>> service = new DepartmentsFilterWithPaginationService
		(
			request.Page,
			request.PageSize,
			TeacherDepartmentsExpressionFactory.CreateFilterExpression(parameter),
			_repository
		);
		return TeacherDepartmentResponse.FromResult(await service.DoOperation());
	}

	[HttpGet("search")]
	public async Task<ActionResult<IReadOnlyCollection<TeacherDepartmentResponse>>> SearchDepartments([FromQuery] DepartmentSearchRequest request)
	{
		TeachersDepartmentRepositoryParameter parameter = DepartmentSchemaConverter.ToRepositoryParameter(request.Department);
		IService<IReadOnlyCollection<TeachersDepartment>> service = new DepartmentsFilterService
		(
			TeacherDepartmentsExpressionFactory.CreateFilterExpression(parameter),
			_repository
		);
		return TeacherDepartmentResponse.FromResult(await service.DoOperation());
	}

	[HttpPost]
	public async Task<ActionResult<TeacherDepartmentResponse>> CreateTeacherDepartment([FromBody] DepartmentCreateRequest request)
	{
		TeachersDepartmentRepositoryParameter parameter = DepartmentSchemaConverter.ToRepositoryParameter(request.Department);
		IService<TeachersDepartment> service = new DepartmentCreationService
		(
			request.Department,
			_repository,
			TeacherDepartmentsExpressionFactory.CreateHasDepartmentExpression(parameter)
		);
		return TeacherDepartmentResponse.FromResult(await service.DoOperation());
	}

	[HttpDelete]
	public async Task<ActionResult<TeacherDepartmentResponse>> DeleteTeacherDepartment([FromBody] DepartmentDeleteRequest request)
	{
		TeachersDepartmentRepositoryParameter parameter = DepartmentSchemaConverter.ToRepositoryParameter(request.Department);
		IService<TeachersDepartment> service = new DepartmentDeletionService
		(
			request.Department,
			_repository,
			TeacherDepartmentsExpressionFactory.CreateHasDepartmentExpression(parameter)
		);
		return TeacherDepartmentResponse.FromResult(await service.DoOperation());
	}

	[HttpPut]
	public async Task<ActionResult<TeacherDepartmentResponse>> ChangeDepartmentName([FromBody] DepartmentUpdateRequest request)
	{
		TeachersDepartmentRepositoryParameter oldParameter = DepartmentSchemaConverter.ToRepositoryParameter(request.OldDepartment);
		TeachersDepartmentRepositoryParameter newParameter = DepartmentSchemaConverter.ToRepositoryParameter(request.NewDepartment);
		IService<TeachersDepartment> service = new DepartmentChangeNameService
		(
			request.NewDepartment,
			_repository,
			TeacherDepartmentsExpressionFactory.CreateHasDepartmentExpression(oldParameter),
			TeacherDepartmentsExpressionFactory.CreateHasDepartmentExpression(newParameter)
		);
		return TeacherDepartmentResponse.FromResult(await service.DoOperation());
	}
}

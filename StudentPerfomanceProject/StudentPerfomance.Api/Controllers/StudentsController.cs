using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.StudentRequests;
using StudentPerfomance.Api.Responses.Student;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Student.ChangeStudentData;
using StudentPerfomance.Application.Commands.Student.CreateStudent;
using StudentPerfomance.Application.Commands.Student.DeleteStudent;
using StudentPerfomance.Application.Queries.Student.GetStudentsByFilter;
using StudentPerfomance.Application.Queries.Student.GetStudentsByPage;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.DataAccess.Repositories.Students;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class StudentsController : Controller
{
	private readonly IRepository<Student> _studentsRepository = new StudentsRepository();
	private readonly IRepository<StudentGroup> _groupsRepository = new StudentGroupsRepository();

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> GetTotalStudentsCount([FromQuery] StudentCountByGroupRequest request)
	{
		StudentGroupsRepositoryParameter parameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IRepositoryExpression<Student> expression = StudentsRepositoryExpressionFactory.CreateByGroupExpression(parameter);
		int count = await _studentsRepository.CountWithExpression(expression);
		return Ok(count);
	}

	[HttpGet("byPage")]
	public async Task<ActionResult<IReadOnlyCollection<StudentResponse>>> GetStudentsByPage([FromQuery] StudentByPageAndGroupRequest request)
	{
		StudentGroupsRepositoryParameter parameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IService<IReadOnlyCollection<Student>> service = new StudentsPaginationService
		(
			request.Page,
			request.PageSize,
			request.Group,
			_studentsRepository,
			StudentsRepositoryExpressionFactory.CreateByGroupExpression(parameter)
		);
		return StudentResponse.FromResult(await service.DoOperation());
	}

	[HttpGet("byFilter")]
	public async Task<ActionResult<IReadOnlyCollection<StudentResponse>>> GetStudentsByFilter([FromQuery] StudentFilterAndPageRequest request)
	{
		StudentsRepositoryParameter studentParameter = StudentSchemaConverter.ToRepositoryParameter(request.Student);
		StudentGroupsRepositoryParameter groupsParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IService<IReadOnlyCollection<Student>> service = new StudentsFilterService
		(
			request.Page,
			request.PageSize,
			_studentsRepository,
			StudentsRepositoryExpressionFactory.CreateFilterExpression(studentParameter, groupsParameter)
		);
		return StudentResponse.FromResult(await service.DoOperation());
	}

	[HttpPost]
	public async Task<ActionResult<StudentResponse>> CreateStudent([FromBody] StudentCreateRequest request)
	{
		StudentsRepositoryParameter studentParameter = StudentSchemaConverter.ToRepositoryParameter(request.Student);
		StudentGroupsRepositoryParameter groupParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IService<Student> service = new StudentCreationService
		(
			request.Student,
			request.Group,
			_studentsRepository,
			_groupsRepository,
			StudentGroupsExpressionFactory.CreateHasGroupExpression(groupParameter),
			StudentsRepositoryExpressionFactory.CreateHasStudentExpression(studentParameter),
			StudentsRepositoryExpressionFactory.CreateHasRecordbookExpression(studentParameter)
		);
		return StudentResponse.FromResult(await service.DoOperation());
	}

	[HttpDelete]
	public async Task<ActionResult<StudentResponse>> DeleteStudent([FromBody] StudentDeleteRequest request)
	{
		StudentsRepositoryParameter parameter = StudentSchemaConverter.ToRepositoryParameter(request.Student);
		IService<Student> service = new StudentDeletionService
		(
			request.Student,
			_studentsRepository,
			StudentsRepositoryExpressionFactory.CreateHasRecordbookExpression(parameter)
		);
		return StudentResponse.FromResult(await service.DoOperation());
	}

	[HttpPut]
	public async Task<ActionResult<StudentResponse>> ChangeStudentData([FromBody] StudentUpdateRequest request)
	{
		StudentsRepositoryParameter oldParameter = StudentSchemaConverter.ToRepositoryParameter(request.OldData);
		StudentsRepositoryParameter newParameter = StudentSchemaConverter.ToRepositoryParameter(request.NewData);
		StudentGroupsRepositoryParameter groupParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IService<Student> service = new StudentUpdateService
		(
			request.NewData,
			request.Group,
			_studentsRepository,
			_groupsRepository,
			StudentsRepositoryExpressionFactory.CreateHasStudentExpression(oldParameter),
			StudentsRepositoryExpressionFactory.CreateHasRecordbookExpression(newParameter),
			StudentGroupsExpressionFactory.CreateHasGroupExpression(groupParameter)
		);
		return StudentResponse.FromResult(await service.DoOperation());
	}
}

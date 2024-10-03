using Microsoft.AspNetCore.Mvc;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Students;

using Response = Microsoft.AspNetCore.Mvc.ActionResult<SPerfomance.Api.Module.Responses.Students.StudentResponse>;
using CollectionResponse = Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IReadOnlyCollection<SPerfomance.Api.Module.Responses.Students.StudentResponse>>;
using SPerfomance.Api.Module.Converters.StudentGroups;
using SPerfomance.Api.Module.Converters.Students;
using SPerfomance.Api.Module.Responses.Students;
using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Students.Module.Commands.Create;
using SPerfomance.Application.Students.Module.Commands.Delete;
using SPerfomance.Application.Students.Module.Commands.Update;
using SPerfomance.Application.Students.Module.Queries.All;
using SPerfomance.Application.Students.Module.Queries.Count;
using SPerfomance.Application.Students.Module.Queries.Filter;
using SPerfomance.Application.Students.Module.Queries.Paged;
using SPerfomance.Application.Students.Module.Queries.Search;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups.Expressions;
using SPerfomance.DataAccess.Module.Shared.Repositories.Students;
using SPerfomance.DataAccess.Module.Shared.Repositories.Students.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class StudentsController : Controller
{
	private readonly IRepository<Student> _students;
	private readonly IRepository<StudentGroup> _groups;
	public StudentsController(IRepository<Student> students, IRepository<StudentGroup> groups)
	{
		_students = students;
		_groups = groups;
	}

	[HttpPost]
	public async Task<Response> Create([FromBody] StudentSchema schema)
	{
		StudentsRepositoryObject studentParam = schema.ToRepositoryObject();
		IRepositoryExpression<Student> dublicate = StudentsRepositoryExpressionFactory.CreateHasRecordbookExpression(studentParam);
		IRepositoryExpression<StudentGroup> getGroup = StudentGroupsRepositoryExpressionFactory.CreateFindByNameExpression(studentParam.Group);
		StudentCreateCommand command = new StudentCreateCommand(schema, dublicate, getGroup, _students, _groups);
		return StudentResponse.FromResult(await command.Handler.Handle(command));
	}

	[HttpDelete]
	public async Task<Response> Delete([FromBody] StudentSchema schema)
	{
		StudentsRepositoryObject param = schema.ToRepositoryObject();
		IRepositoryExpression<Student> getStudent = StudentsRepositoryExpressionFactory.CreateHasStudentExpression(param);
		StudentDeleteCommand command = new StudentDeleteCommand(getStudent, _students);
		return StudentResponse.FromResult(await command.Handler.Handle(command));
	}

	[HttpPut]
	public async Task<Response> Update([FromQuery] StudentSchema oldSchema, [FromQuery] StudentSchema newSchema)
	{
		StudentsRepositoryObject initial = oldSchema.ToRepositoryObject();
		StudentsRepositoryObject updated = newSchema.ToRepositoryObject();
		IRepositoryExpression<Student> getInitial = StudentsRepositoryExpressionFactory.CreateHasStudentExpression(initial);
		IRepositoryExpression<Student> findDublicate = StudentsRepositoryExpressionFactory.CreateHasRecordbookExpression(updated);
		IRepositoryExpression<StudentGroup> findGroup = StudentGroupsRepositoryExpressionFactory.CreateFindByNameExpression(updated.Group);
		StudentUpdateCommand command = new StudentUpdateCommand(newSchema, getInitial, findDublicate, findGroup, _students, _groups);
		OperationResult<Student> result = await command.Handler.Handle(command);
		if (result.Result != null && !result.IsFailed)
			await _students.Commit();
		return StudentResponse.FromResult(result);
	}
	[HttpGet("count")]
	public async Task<ActionResult<int>> Count()
	{
		GetStudentsCountQuery query = new GetStudentsCountQuery(_students);
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result.Result);
	}
	[HttpGet("all")]
	public async Task<CollectionResponse> GetAll()
	{
		GetAllStudentsQuery query = new GetAllStudentsQuery(_students);
		return StudentResponse.FromResult(await query.Handler.Handle(query));
	}
	[HttpGet("byPage")]

	public async Task<CollectionResponse> GetPaged(int page, int pageSize)
	{
		GetStudentsPagedQuery query = new GetStudentsPagedQuery(page, pageSize, _students);
		return StudentResponse.FromResult(await query.Handler.Handle(query));
	}
	[HttpGet("filter")]
	public async Task<CollectionResponse> GetFiltered([FromQuery] StudentSchema schema, int page, int pageSize)
	{
		StudentsRepositoryObject parameter = schema.ToRepositoryObject();
		IRepositoryExpression<Student> expression = StudentsRepositoryExpressionFactory.CreateFilterExpression(parameter);
		FilterStudentsQuery query = new FilterStudentsQuery(page, pageSize, expression, _students);
		return StudentResponse.FromResult(await query.Handler.Handle(query));
	}
	[HttpGet("search")]
	public async Task<CollectionResponse> Search([FromQuery] StudentSchema schema)
	{
		StudentsRepositoryObject parameter = schema.ToRepositoryObject();
		IRepositoryExpression<Student> expression = StudentsRepositoryExpressionFactory.CreateFilterExpression(parameter);
		SearchStudentsQuery query = new SearchStudentsQuery(expression, _students);
		return StudentResponse.FromResult(await query.Handler.Handle(query));
	}
}

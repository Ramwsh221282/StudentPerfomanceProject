using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Schemas.Teachers;

using Response = Microsoft.AspNetCore.Mvc.ActionResult<SPerfomance.Api.Module.Responses.Teachers.TeacherResponse>;
using CollectionResponse = Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IReadOnlyCollection<SPerfomance.Api.Module.Responses.Teachers.TeacherResponse>>;
using SPerfomance.Api.Module.Converters.Teachers;
using SPerfomance.Api.Module.Responses.Teachers;
using SPerfomance.Application.Teachers.Module.Commands.Create;
using SPerfomance.Application.Teachers.Module.Commands.Delete;
using SPerfomance.Application.Teachers.Module.Commands.Update;
using SPerfomance.Application.Teachers.Module.Queries.GetFiltered;
using SPerfomance.Application.Teachers.Module.Queries.GetPaged;
using SPerfomance.Application.Teachers.Module.Queries.Search;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers.Expressions;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Application.Teachers.Module.Queries.Count;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TeachersController : Controller
{
	private readonly IRepository<Teacher> _teachers;
	private readonly IRepository<TeachersDepartment> _departments;

	public TeachersController(IRepository<Teacher> teachers, IRepository<TeachersDepartment> departments)
	{
		_teachers = teachers;
		_departments = departments;
	}

	[HttpPost]
	public async Task<Response> Create([FromBody] TeacherSchema teacher)
	{
		TeacherRepositoryObject parameter = teacher.ToRepositoryObject();
		IRepositoryExpression<Teacher> findDublicate = TeachersRepositoryExpressionFactory.CreateHasTeacher(parameter);
		IRepositoryExpression<TeachersDepartment> getDepartment = TeachersDepartmentsExpressionsFactory.FindByName(parameter.Department);
		TeacherCreateCommand command = new TeacherCreateCommand(teacher, findDublicate, getDepartment, _teachers, _departments);
		return TeacherResponse.FromResult(await command.Handler.Handle(command));
	}

	[HttpDelete]
	public async Task<Response> Delete([FromBody] TeacherSchema teacher)
	{
		TeacherRepositoryObject parameter = teacher.ToRepositoryObject();
		IRepositoryExpression<Teacher> getTeacher = TeachersRepositoryExpressionFactory.CreateHasTeacher(parameter);
		TeacherDeleteCommand command = new TeacherDeleteCommand(getTeacher, _teachers);
		return TeacherResponse.FromResult(await command.Handler.Handle(command));
	}

	[HttpPut]
	public async Task<Response> Update([FromQuery] TeacherSchema oldSchema, [FromQuery] TeacherSchema newSchema)
	{
		TeacherRepositoryObject oldParameter = oldSchema.ToRepositoryObject();
		TeacherRepositoryObject newParameter = newSchema.ToRepositoryObject();
		IRepositoryExpression<Teacher> getInitial = TeachersRepositoryExpressionFactory.CreateHasTeacher(oldParameter);
		IRepositoryExpression<Teacher> getDublicate = TeachersRepositoryExpressionFactory.CreateHasTeacher(newParameter);
		TeacherUpdateCommand command = new TeacherUpdateCommand(getInitial, getDublicate, newSchema, _teachers);
		return TeacherResponse.FromResult(await command.Handler.Handle(command));
	}

	[HttpGet("byPage")]
	public async Task<CollectionResponse> GetPaged(int page, int pageSize)
	{
		GetPagedTeachersQuery query = new GetPagedTeachersQuery(page, pageSize, _teachers);
		return TeacherResponse.FromResult(await query.Handler.Handle(query));
	}

	[HttpGet("filter")]
	public async Task<CollectionResponse> GetFiltered([FromQuery] TeacherSchema schema, int page, int pageSize)
	{
		TeacherRepositoryObject parameter = schema.ToRepositoryObject();
		IRepositoryExpression<Teacher> expression = TeachersRepositoryExpressionFactory.CreateFilter(parameter);
		FilterTeachersQuery query = new FilterTeachersQuery(page, pageSize, expression, _teachers);
		return TeacherResponse.FromResult(await query.Handler.Handle(query));
	}

	[HttpGet("search")]
	public async Task<CollectionResponse> Search([FromQuery] TeacherSchema schema)
	{
		TeacherRepositoryObject parameter = schema.ToRepositoryObject();
		IRepositoryExpression<Teacher> expression = TeachersRepositoryExpressionFactory.CreateFilter(parameter);
		SearchTeachersQuery query = new SearchTeachersQuery(expression, _teachers);
		return TeacherResponse.FromResult(await query.Handler.Handle(query));
	}

	[HttpGet("count")]
	public async Task<ActionResult<int>> Count()
	{
		GetTeachersCountQuery query = new GetTeachersCountQuery(_teachers);
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result);
	}
}

using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Schemas.Departments;

using Response = Microsoft.AspNetCore.Mvc.ActionResult<SPerfomance.Api.Module.Responses.TeacherDepartments.TeacherDepartmentResponse>;
using CollectionResponse = Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IReadOnlyCollection<SPerfomance.Api.Module.Responses.TeacherDepartments.TeacherDepartmentResponse>>;
using SPerfomance.Api.Module.Converters.TeacherDepartments;
using SPerfomance.Api.Module.Responses.TeacherDepartments;
using SPerfomance.Application.TeacherDepartments.Module.Commands.Create;
using SPerfomance.Application.TeacherDepartments.Module.Commands.Delete;
using SPerfomance.Application.TeacherDepartments.Module.Commands.Update;
using SPerfomance.Application.TeacherDepartments.Module.Queries.All;
using SPerfomance.Application.TeacherDepartments.Module.Queries.Count;
using SPerfomance.Application.TeacherDepartments.Module.Queries.Filtered;
using SPerfomance.Application.TeacherDepartments.Module.Queries.Paged;
using SPerfomance.Application.TeacherDepartments.Module.Queries.Searched;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TeacherDepartmentsController : Controller
{
	private readonly IRepository<TeachersDepartment> _repository;

	public TeacherDepartmentsController(IRepository<TeachersDepartment> repository)
	{
		_repository = repository;
	}

	[HttpGet("all")]
	public async Task<CollectionResponse> GetAll()
	{
		GetAllTeacherDepartmentsQuery query = new GetAllTeacherDepartmentsQuery(_repository);
		return TeacherDepartmentResponse.FromResult(await query.Handler.Handle(query));
	}

	[HttpGet("count")]
	public async Task<ActionResult<int>> Count()
	{
		GetTeachersDepartmentsCountQuery query = new GetTeachersDepartmentsCountQuery(_repository);
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result.Result);
	}

	[HttpGet("filter")]
	public async Task<CollectionResponse> Filter([FromQuery] DepartmentSchema schema, int page, int pageSize)
	{
		DepartmentRepositoryObject parameter = schema.ToRepositoryObject();
		IRepositoryExpression<TeachersDepartment> expression = TeachersDepartmentsExpressionsFactory.Filter(parameter);
		FilterTeacherDepartmentsQuery query = new FilterTeacherDepartmentsQuery(page, pageSize, expression, _repository);
		return TeacherDepartmentResponse.FromResult(await query.Handler.Handle(query));
	}

	[HttpGet("search")]
	public async Task<CollectionResponse> Search([FromQuery] DepartmentSchema schema)
	{
		DepartmentRepositoryObject parameter = schema.ToRepositoryObject();
		IRepositoryExpression<TeachersDepartment> expression = TeachersDepartmentsExpressionsFactory.Filter(parameter);
		SearchTeachersDepartmentsQuery query = new SearchTeachersDepartmentsQuery(expression, _repository);
		return TeacherDepartmentResponse.FromResult(await query.Handler.Handle(query));
	}

	[HttpGet("byPage")]
	public async Task<CollectionResponse> GetPaged(int page, int pageSize)
	{
		GetTeachersDepartmentPagedQuery query = new GetTeachersDepartmentPagedQuery(page, pageSize, _repository);
		return TeacherDepartmentResponse.FromResult(await query.Handler.Handle(query));
	}

	[HttpPost]
	public async Task<Response> Create([FromBody] DepartmentSchema schema)
	{
		DepartmentRepositoryObject parameter = schema.ToRepositoryObject();
		IRepositoryExpression<TeachersDepartment> expression = TeachersDepartmentsExpressionsFactory.FindByName(parameter);
		TeacherDepartmentCreateCommand command = new TeacherDepartmentCreateCommand(schema, expression, _repository);
		return TeacherDepartmentResponse.FromResult(await command.Handler.Handle(command));
	}

	[HttpPut]
	public async Task<Response> Update([FromQuery] DepartmentSchema oldSchema, [FromQuery] DepartmentSchema newSchema)
	{
		DepartmentRepositoryObject oldParameter = oldSchema.ToRepositoryObject();
		DepartmentRepositoryObject newParameter = newSchema.ToRepositoryObject();
		IRepositoryExpression<TeachersDepartment> findInitial = TeachersDepartmentsExpressionsFactory.HasDepartment(oldParameter);
		IRepositoryExpression<TeachersDepartment> checkDublicate = TeachersDepartmentsExpressionsFactory.FindByName(newParameter);
		TeachersDepartmentUpdateCommand command = new TeachersDepartmentUpdateCommand(newSchema, findInitial, checkDublicate, _repository);
		return TeacherDepartmentResponse.FromResult(await command.Handler.Handle(command));
	}

	[HttpDelete]
	public async Task<Response> Delete([FromBody] DepartmentSchema schema)
	{
		DepartmentRepositoryObject parameter = schema.ToRepositoryObject();
		IRepositoryExpression<TeachersDepartment> getDepartment = TeachersDepartmentsExpressionsFactory.HasDepartment(parameter);
		TeacherDepartmentDeleteCommand command = new TeacherDepartmentDeleteCommand(getDepartment, _repository);
		return TeacherDepartmentResponse.FromResult(await command.Handler.Handle(command));
	}
}

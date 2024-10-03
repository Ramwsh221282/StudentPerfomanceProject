using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;

using Response = Microsoft.AspNetCore.Mvc.ActionResult<SPerfomance.Api.Module.Responses.StudentGroups.StudentGroupResponse>;
using CollectionResponse = Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IReadOnlyCollection<SPerfomance.Api.Module.Responses.StudentGroups.StudentGroupResponse>>;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Queries.Count;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Application.StudentGroups.Module.Queries.All;
using SPerfomance.Api.Module.Responses.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Queries.PagedFiltered;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.Api.Module.Converters.StudentGroups;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups.Expressions;
using SPerfomance.Application.StudentGroups.Module.Queries.Searched;
using SPerfomance.Application.StudentGroups.Module.Queries.Paged;
using SPerfomance.Application.StudentGroups.Module.Commands.Create;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans.Expressions;
using SPerfomance.Application.StudentGroups.Module.Commands.Delete;
using SPerfomance.Application.StudentGroups.Module.Commands.Update;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class StudentGroupsController : Controller
{
	private readonly IRepository<EducationPlan> _plans;
	private readonly IRepository<StudentGroup> _groups;
	public StudentGroupsController(IRepository<EducationPlan> plans, IRepository<StudentGroup> groups)
	{
		_plans = plans;
		_groups = groups;
	}

	[HttpGet("count")]
	public async Task<ActionResult<int>> GetCount()
	{
		GetStudentGroupsCountQuery query = new GetStudentGroupsCountQuery(_groups);
		OperationResult<int> result = await query.Handler.Handle(query);
		return result.Result;
	}
	[HttpGet("all")]
	public async Task<CollectionResponse> GetAll()
	{
		GetAllStudentGroupsQuery query = new GetAllStudentGroupsQuery(_groups);
		return StudentGroupResponse.FromResult(await query.Handler.Handle(query));
	}
	[HttpGet("filter")]
	public async Task<CollectionResponse> GetByFilter([FromQuery] StudentsGroupSchema schema, int page, int pageSize)
	{
		StudentGroupsRepositoryObject parameter = schema.ToRepositoryObject();
		IRepositoryExpression<StudentGroup> expression = StudentGroupsRepositoryExpressionFactory.CreateFilterExpression(parameter);
		GetStudentGroupsPagedFilteredQuery query = new GetStudentGroupsPagedFilteredQuery(page, pageSize, expression, _groups);
		return StudentGroupResponse.FromResult(await query.Handler.Handle(query));
	}
	[HttpGet("search")]
	public async Task<CollectionResponse> GetBySearch([FromQuery] StudentsGroupSchema schema)
	{
		StudentGroupsRepositoryObject parameter = schema.ToRepositoryObject();
		IRepositoryExpression<StudentGroup> expression = StudentGroupsRepositoryExpressionFactory.CreateFilterExpression(parameter);
		SearchStudentGroupsQuery query = new SearchStudentGroupsQuery(expression, _groups);
		return StudentGroupResponse.FromResult(await query.Handler.Handle(query));
	}
	[HttpGet("paged")]
	public async Task<CollectionResponse> GetPaged(int page, int pageSize)
	{
		GetStudentGroupsPagedQuery query = new GetStudentGroupsPagedQuery(page, pageSize, _groups);
		return StudentGroupResponse.FromResult(await query.Handler.Handle(query));
	}
	[HttpPost]
	public async Task<Response> Create([FromBody] StudentsGroupSchema schema)
	{
		StudentGroupsRepositoryObject parameter = schema.ToRepositoryObject();
		IRepositoryExpression<StudentGroup> nameDublicate = StudentGroupsRepositoryExpressionFactory.CreateFindByNameExpression(parameter);
		IRepositoryExpression<EducationPlan> getPlan = EducationPlanExpressionsFactory.CreateFindPlan(parameter.EducationPlan);
		StudentGroupCreationCommand command = new StudentGroupCreationCommand(schema, nameDublicate, getPlan, _groups, _plans);
		return StudentGroupResponse.FromResult(await command.Handler.Handle(command));
	}
	[HttpDelete]
	public async Task<Response> Delete([FromBody] StudentsGroupSchema schema)
	{
		StudentGroupsRepositoryObject parameter = schema.ToRepositoryObject();
		IRepositoryExpression<StudentGroup> expression = StudentGroupsRepositoryExpressionFactory.CreateHasGroupExpression(parameter);
		StudentGroupDeletionCommand command = new StudentGroupDeletionCommand(expression, _groups);
		return StudentGroupResponse.FromResult(await command.Handler.Handle(command));
	}

	[HttpPut]
	public async Task<Response> Update([FromQuery] StudentsGroupSchema oldSchema, [FromQuery] StudentsGroupSchema newSchema)
	{
		StudentGroupsRepositoryObject oldParameter = oldSchema.ToRepositoryObject();
		StudentGroupsRepositoryObject newParameter = newSchema.ToRepositoryObject();
		IRepositoryExpression<StudentGroup> getInitial = StudentGroupsRepositoryExpressionFactory.CreateHasGroupExpression(oldParameter);
		IRepositoryExpression<StudentGroup> nameDublicate = StudentGroupsRepositoryExpressionFactory.CreateFindByNameExpression(newParameter);
		StudentGroupUpdateCommand command = new StudentGroupUpdateCommand(newSchema, getInitial, nameDublicate, _groups);
		return StudentGroupResponse.FromResult(await command.Handler.Handle(command));
	}
}

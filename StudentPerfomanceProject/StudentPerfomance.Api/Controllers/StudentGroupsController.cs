using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.GroupRequests;
using StudentPerfomance.Api.Responses.StudentGroup;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Group.CreateStudentGroup;
using StudentPerfomance.Application.Commands.Group.DeleteStudentGroup;
using StudentPerfomance.Application.Commands.Group.MergeStudentGroups;
using StudentPerfomance.Application.Queries.Group.GetGroupByName;
using StudentPerfomance.Application.Queries.Group.GetGroupsByFilter;
using StudentPerfomance.Application.Queries.Group.GetGroupsByPage;
using StudentPerfomance.Application.Queries.Group.GetGroupsStartsWithSearchParam;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class StudentGroupsController : Controller
{
	private readonly IRepository<StudentGroup> _groups = new StudentGroupsRepository();
	private readonly IRepository<Semester> _semesters = new SemestersRepository();
	private readonly IRepository<EducationPlan> _educationPlans = new EducationPlansRepository();

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> GetTotalCount()
	{
		int count = await _groups.Count();
		return Ok(count);
	}

	[HttpGet("byPage")]
	public async Task<ActionResult<IReadOnlyCollection<StudentGroupResponse>>> GetGroupsByPage([FromQuery] GroupByPageRequest request)
	{
		IService<IReadOnlyCollection<StudentGroup>> service = new StudentGroupsPaginationService(request.Page, request.PageSize, _groups);
		return StudentGroupResponse.FromResult(await service.DoOperation());
	}

	[HttpGet("all")]
	public async Task<ActionResult<IReadOnlyCollection<StudentGroupResponse>>> GetAllGroups()
	{
		IReadOnlyCollection<StudentGroup> groups = await _groups.GetAll();
		OperationResult<IReadOnlyCollection<StudentGroup>> result = new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		return StudentGroupResponse.FromResult(result);
	}

	[HttpGet("byFilter")]
	public async Task<ActionResult<IReadOnlyCollection<StudentGroupResponse>>> GetGroupsByFilter([FromQuery] GroupByFilterRequest request)
	{
		StudentGroupsRepositoryParameter parameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IService<IReadOnlyCollection<StudentGroup>> service = new StudentGroupsFilterService
		(
			request.Group,
			request.Page,
			request.PageSize,
			_groups,
			StudentGroupsExpressionFactory.CreateFilterExpression(parameter)
		);
		return StudentGroupResponse.FromResult(await service.DoOperation());
	}

	[HttpGet("byName")]
	public async Task<ActionResult<StudentGroupResponse>> GetGroupByName([FromQuery] GroupByNameRequest request)
	{
		StudentGroupsRepositoryParameter parameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IService<StudentGroup> service = new StudentGroupByNameService
		(
			request.Group,
			_groups,
			StudentGroupsExpressionFactory.CreateFilterExpression(parameter)
		);
		return StudentGroupResponse.FromResult(await service.DoOperation());
	}

	[HttpGet("bySearchNameParam")]
	public async Task<ActionResult<IReadOnlyCollection<StudentGroupResponse>>> GetByNameSearchParam([FromQuery] GroupByNameSearchRequest request)
	{
		StudentGroupsRepositoryParameter parameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IService<IReadOnlyCollection<StudentGroup>> service = new StudentGroupsSearchByNameService
		(
			request.Group,
			_groups,
			StudentGroupsExpressionFactory.CreateSearchWithNameParamExpression(parameter)
		);
		return StudentGroupResponse.FromResult(await service.DoOperation());
	}

	[HttpPost]
	public async Task<ActionResult<StudentGroupResponse>> CreateGroup([FromBody] GroupCreateRequest request)
	{
		StudentGroupsRepositoryParameter groupParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		EducationPlanRepositoryParameter planParameter = EducationPlanSchemaConverter.ToRepositoryParameter(request.EducationPlan);
		EducationDirectionRepositoryParameter directionParameter = EducationDirectionSchemaConverter.ToRepositoryParameter(request.EducationPlan.Direction);
		IService<StudentGroup> service = new StudentGroupCreationService
		(
			request.Group,
			request.EducationPlan,
			_groups,
			_educationPlans,
			StudentGroupsExpressionFactory.CreateHasGroupExpression(groupParameter),
			EducationPlanExpressionsFactory.CreateFindPlan(planParameter, directionParameter)
		);
		return StudentGroupResponse.FromResult(await service.DoOperation());
	}

	[HttpDelete]
	public async Task<ActionResult<StudentGroupResponse>> DeleteGroup([FromBody] GroupDeleteRequest request)
	{
		StudentGroupsRepositoryParameter parameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IService<StudentGroup> service = new StudentGroupsDeleteService
		(
			request.Group,
			_groups,
			StudentGroupsExpressionFactory.CreateHasGroupExpression(parameter)
		);
		return StudentGroupResponse.FromResult(await service.DoOperation());
	}

	[HttpPut]
	public async Task<ActionResult<StudentGroupResponse>> ChangeGroupName([FromBody] GroupUpdateRequest request)
	{
		throw new NotImplementedException();
	}

	[HttpPut("mergeGroups")]
	public async Task<ActionResult<StudentGroupResponse>> MergeGroups([FromBody] GroupMergeRequest request)
	{
		StudentGroupsRepositoryParameter parameterA = StudentsGroupSchemaConverter.ToRepositoryParameter(request.TargetGroup);
		StudentGroupsRepositoryParameter parameterB = StudentsGroupSchemaConverter.ToRepositoryParameter(request.MergeGroup);
		IService<StudentGroup> service = new StudentGroupsMergeService
		(
			request.TargetGroup,
			request.MergeGroup,
			_groups,
			StudentGroupsExpressionFactory.CreateHasGroupExpression(parameterA),
			StudentGroupsExpressionFactory.CreateHasGroupExpression(parameterB)
		);
		return StudentGroupResponse.FromResult(await service.DoOperation());
	}
}

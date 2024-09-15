using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.SemesterPlanRequests;
using StudentPerfomance.Api.Responses.SemesterPlans;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.SemesterPlans.CreateSemesterPlan;
using StudentPerfomance.Application.Commands.SemesterPlans.DeleteSemesterPlan;
using StudentPerfomance.Application.Commands.SemesterPlans.SetTeacher;
using StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlansByFilter;
using StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlansByPage;
using StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlansCountByGroupSemester;
using StudentPerfomance.DataAccess.Repositories.Disciplines;
using StudentPerfomance.DataAccess.Repositories.SemesterPlans;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.DataAccess.Repositories.Teachers;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SemesterPlansController : Controller
{
	private readonly IRepository<SemesterPlan> _plans = new SemesterPlansRepository();
	private readonly IRepository<Semester> _semesters = new SemestersRepository();
	private readonly IRepository<Discipline> _disciplines = new DisciplineRepository();
	private readonly IRepository<Teacher> _teachers = new TeachersRepository();

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> GetSemesterPlansAmount([FromQuery] SemesterPlanCountBySemesterAndGroup request)
	{
		SemestersRepositoryParameter semesterParameter = SemesterSchemaConverter.ToRepositoryParameter(request.Semester);
		StudentGroupsRepositoryParameter groupParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IService<int> service = new SemesterPlansCountByGroupSemesterService
		(
			SemesterPlansExpressionFactory.CreateByGroupAndSemesterExpression(semesterParameter, groupParameter),
			_plans
		);
		OperationResult<int> count = await service.DoOperation();
		return count.Result;
	}

	[HttpGet("byPage")]
	public async Task<ActionResult<IReadOnlyCollection<SemesterPlanResponse>>> GetSemesterPlans([FromQuery] SemesterPlanByPageAndGroupSemesterRequest request)
	{
		SemestersRepositoryParameter semesterParameter = SemesterSchemaConverter.ToRepositoryParameter(request.Semester);
		StudentGroupsRepositoryParameter groupParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IService<IReadOnlyCollection<SemesterPlan>> service = new SemesterPlansPaginationService
		(
			request.Page,
			request.PageSize,
			SemesterPlansExpressionFactory.CreateByGroupAndSemesterExpression(semesterParameter, groupParameter),
			_plans
		);
		return SemesterPlanResponse.FromResult(await service.DoOperation());
	}

	[HttpGet("byFilter")]
	public async Task<ActionResult<IReadOnlyCollection<SemesterPlanResponse>>> GetSemesterPlansByFilter([FromQuery] SemesterPlanByPageAndFilter request)
	{
		StudentGroupsRepositoryParameter groupParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		SemestersRepositoryParameter semesterParameter = SemesterSchemaConverter.ToRepositoryParameter(request.Semester);
		DisciplineRepositoryParameter disciplineParameter = DisciplineSchemaConverter.ToRepositoryParameter(request.Discipline);
		IService<IReadOnlyCollection<SemesterPlan>> service = new SemesterPlansFilterService
		(
			request.Page,
			request.PageSize,
			SemesterPlansExpressionFactory.CreateFilterExpression(groupParameter, semesterParameter, disciplineParameter),
			_plans
		);
		return SemesterPlanResponse.FromResult(await service.DoOperation());
	}

	[HttpPost]
	public async Task<ActionResult<SemesterPlanResponse>> CreateSemesterPlan([FromBody] SemesterPlanCreateRequest request)
	{
		SemestersRepositoryParameter semesterParameter = SemesterSchemaConverter.ToRepositoryParameter(request.Semester);
		DisciplineRepositoryParameter disciplineParameter = DisciplineSchemaConverter.ToRepositoryParameter(request.Discipline);
		StudentGroupsRepositoryParameter groupParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(request.Group);
		IService<SemesterPlan> service = new SemesterPlanCreationService
		(
			request.Discipline,
			request.Semester,
			SemesterPlansExpressionFactory.CreateHasExpression(semesterParameter, disciplineParameter),
			DisciplineExpressionFactory.CreateHasExpression(disciplineParameter),
			SemesterExpressionFactory.CreateHasSemesterExpression(semesterParameter, groupParameter),
			_plans,
			_semesters,
			_disciplines
		);
		return SemesterPlanResponse.FromResult(await service.DoOperation());
	}

	[HttpDelete]
	public async Task<ActionResult<SemesterPlanResponse>> DeleteSemesterPlan([FromBody] SemesterPlanDeleteRequest request)
	{
		SemestersRepositoryParameter semesterParameter = SemesterSchemaConverter.ToRepositoryParameter(request.Semester);
		DisciplineRepositoryParameter disciplineParameter = DisciplineSchemaConverter.ToRepositoryParameter(request.Discipline);
		IService<SemesterPlan> service = new SemesterPlanDeletionService
		(
			SemesterPlansExpressionFactory.CreateHasExpression(semesterParameter, disciplineParameter),
			_plans
		);
		return SemesterPlanResponse.FromResult(await service.DoOperation());
	}

	[HttpPost("setTeacher")]
	public async Task<ActionResult<SemesterPlanResponse>> SetTeacher([FromBody] SemesterPlanSetTeacherRequest request)
	{
		SemestersRepositoryParameter semesterParameter = SemesterSchemaConverter.ToRepositoryParameter(request.Semester);
		DisciplineRepositoryParameter disciplineParameter = DisciplineSchemaConverter.ToRepositoryParameter(request.Discipline);
		TeacherRepositoryParameter teacherParameter = TeacherSchemaConverter.ToRepositoryParameter(request.Teacher);
		IService<SemesterPlan> service = new SemesterPlanTeacherAdjustmentService
		(
			TeacherExpressionFactory.CreateByName(teacherParameter),
			SemesterPlansExpressionFactory.CreateHasExpression(semesterParameter, disciplineParameter),
			_plans,
			_teachers
		);
		return SemesterPlanResponse.FromResult(await service.DoOperation());
	}
}

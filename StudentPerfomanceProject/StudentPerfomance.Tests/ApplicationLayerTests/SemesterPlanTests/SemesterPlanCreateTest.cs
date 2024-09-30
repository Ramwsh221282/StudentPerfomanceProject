using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.SemesterPlanRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.SemesterPlans.CreateSemesterPlan;
using StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.DataAccess.Repositories.Disciplines;
using StudentPerfomance.DataAccess.Repositories.SemesterPlans;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.SemesterPlanTests;

public sealed class SemesterPlanCreateTest(SemesterSchema semester, DisciplineSchema discipline, StudentsGroupSchema groupSchema)
: IService<SemesterPlan>
{
	private readonly SemesterPlanCreateRequest _request = new SemesterPlanCreateRequest(semester, discipline, groupSchema);
	private readonly IRepository<SemesterPlan> _plans = new SemesterPlansRepository();
	private readonly IRepository<Semester> _semesters = new SemestersRepository();
	private readonly IRepository<Discipline> _disciplines = new DisciplineRepository();

	public async Task<OperationResult<SemesterPlan>> DoOperation()
	{
		/*SemestersRepositoryParameter semesterParam = SemesterSchemaConverter.ToRepositoryParameter(_request.Semester);
		DisciplineRepositoryParameter disciplineParam = DisciplineSchemaConverter.ToRepositoryParameter(_request.Discipline);
		StudentGroupsRepositoryParameter groupParam = StudentsGroupSchemaConverter.ToRepositoryParameter(_request.Group);
		IService<SemesterPlan> service = new SemesterPlanCreationService
		(
			_request.Discipline,
			_request.Semester,
			SemesterPlansExpressionFactory.CreateHasExpression(semesterParam, disciplineParam),
			DisciplineExpressionFactory.CreateHasExpression(disciplineParam),
			SemesterExpressionFactory.CreateHasSemesterExpression(semesterParam, groupParam),
			_plans,
			_semesters,
			_disciplines
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<SemesterPlan>, SemesterPlan> logger = new(result, "Semester plan creation test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Created semester plan info:");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Full plan name: {result.Result.PlanName}");
			Console.WriteLine($"Discipline attached: {result.Result.LinkedDiscipline.Name}");
			Console.WriteLine($"Semester number: {result.Result.LinkedSemester.Number.Value}");
		}
		return result;*/
		throw new NotImplementedException();
	}
}

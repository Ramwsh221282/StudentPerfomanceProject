using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.SemesterPlanRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.SemesterPlans.DeleteSemesterPlan;
using StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.DataAccess.Repositories.Disciplines;
using StudentPerfomance.DataAccess.Repositories.SemesterPlans;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.SemesterPlanTests;

public sealed class SemesterPlanDeleteTest(SemesterSchema semester, DisciplineSchema discipline) : IService<SemesterPlan>
{
	private readonly SemesterPlanDeleteRequest _request = new SemesterPlanDeleteRequest(semester, discipline);
	private readonly IRepository<SemesterPlan> _repository = new SemesterPlansRepository();

	public async Task<OperationResult<SemesterPlan>> DoOperation()
	{
		SemestersRepositoryParameter semesterParam = SemesterSchemaConverter.ToRepositoryParameter(_request.Semester);
		DisciplineRepositoryParameter discipline = DisciplineSchemaConverter.ToRepositoryParameter(_request.Discipline);
		IService<SemesterPlan> service = new SemesterPlanDeletionService
		(
			SemesterPlansExpressionFactory.CreateHasExpression(semesterParam, discipline),
			_repository
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<SemesterPlan>, SemesterPlan> logger = new(result, "Semester plan deletion test info:");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Deleted semester plan info: ");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Full plan name: {result.Result.PlanName}");
			Console.WriteLine($"Attached discipline: {result.Result.LinkedDiscipline.Name}");
			Console.WriteLine($"Semester number: {result.Result.LinkedSemester.Number.Value}");
			Console.WriteLine($"Plan of group: {result.Result.LinkedSemester.Group.Name.Name}");
		}
		return result;
	}
}

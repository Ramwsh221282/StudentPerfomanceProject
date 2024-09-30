using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationPlanRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.EducationPlans.Create;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationPlansTests;

internal sealed class CreateEducationPlanWithSemestersTest(EducationPlanGeneralRequest request) : IService<EducationPlan>
{
	private readonly EducationPlanGeneralRequest _request = request;
	private readonly IRepository<EducationPlan> _plans = new EducationPlansRepository();
	private readonly IRepository<EducationDirection> _directions = new EducationDirectionRepository();
	private readonly IRepository<Semester> _semesters = new SemestersRepository();
	public async Task<OperationResult<EducationPlan>> DoOperation()
	{
		EducationPlanRepositoryParameter checkDublicateParameter = EducationPlanSchemaConverter.ToRepositoryParameter(_request.Plan);
		EducationDirectionRepositoryParameter findDirectionParameter = EducationDirectionSchemaConverter.ToRepositoryParameter(_request.Plan.Direction);
		IService<EducationPlan> service = new CreateEducationPlanService
		(
			_request.Plan,
			EducationPlanExpressionsFactory.CreateFindPlan(checkDublicateParameter, findDirectionParameter),
			EducationDirectionExpressionsFactory.FindDirection(findDirectionParameter),
			_plans,
			_directions,
			_semesters
		);
		OperationResult<EducationPlan> result = await service.DoOperation();
		OperationResultLogger<OperationResult<EducationPlan>, EducationPlan> logger =
		new(result, "Education Plan Creation Test");
		logger.ShowInfo();
		if (result.Result != null && !result.IsFailed)
		{
			Console.WriteLine("Created education plan log info:");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Entity Number: {result.Result.EntityNumber}");
			Console.WriteLine($"Year: {result.Result.Year}");
			Console.WriteLine($"Direction name: {result.Result.Direction.Name}");
			Console.WriteLine($"Direction type: {result.Result.Direction.Type}");
			Console.WriteLine($"Direction code: {result.Result.Direction.Code}");
			Console.WriteLine();
			Console.WriteLine("Semesters info");
			foreach (var semester in result.Result.Semesters)
			{
				Console.WriteLine($"ID: {semester.Id}");
				Console.WriteLine($"Entity number: {semester.EntityNumber}");
				Console.WriteLine($"Semester Number: {semester.Number.Value}");
			}
		}
		return result;
	}
}

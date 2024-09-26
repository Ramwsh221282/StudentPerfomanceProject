using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationPlanRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.EducationPlans.Create;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationPlansTests;

public sealed class CreateEducationPlanTest(EducationPlanGeneralRequest request) : IService<EducationPlan>
{
	private readonly EducationPlanSchema _plan = request.Plan;
	private readonly IRepository<EducationPlan> _plans = new EducationPlansRepository();
	private readonly IRepository<EducationDirection> _directions = new EducationDirectionRepository();
	public async Task<OperationResult<EducationPlan>> DoOperation()
	{
		EducationPlanRepositoryParameter checkDublicateParameter = EducationPlanSchemaConverter.ToRepositoryParameter(_plan);
		EducationDirectionRepositoryParameter findDirectionParameter = EducationDirectionSchemaConverter.ToRepositoryParameter(_plan.Direction);
		IService<EducationPlan> service = new CreateEducationPlanService
		(
			_plan,
			EducationPlanExpressionsFactory.CreateFindPlan(checkDublicateParameter, findDirectionParameter),
			EducationDirectionExpressionsFactory.FindDirection(findDirectionParameter),
			_plans,
			_directions
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
		}
		return result;
	}
}

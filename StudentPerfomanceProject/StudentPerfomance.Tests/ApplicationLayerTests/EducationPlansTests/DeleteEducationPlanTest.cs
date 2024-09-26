using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationPlanRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.EducationPlans.Delete;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationPlansTests;

public sealed class DeleteEducationPlanTest(EducationPlanGeneralRequest request) : IService<EducationPlan>
{
	private readonly EducationPlanSchema _plan = request.Plan;
	private readonly IRepository<EducationPlan> _repository = new EducationPlansRepository();
	public async Task<OperationResult<EducationPlan>> DoOperation()
	{
		EducationPlanRepositoryParameter planParameter = EducationPlanSchemaConverter.ToRepositoryParameter(_plan);
		EducationDirectionRepositoryParameter directionParameter = EducationDirectionSchemaConverter.ToRepositoryParameter(_plan.Direction);
		IService<EducationPlan> service = new DeleteEducationPlanService
		(
			EducationPlanExpressionsFactory.CreateFindPlan(planParameter, directionParameter),
			_repository
		);
		OperationResult<EducationPlan> result = await service.DoOperation();
		OperationResultLogger<OperationResult<EducationPlan>, EducationPlan> logger =
		new(result, "Education Plan deletion info");
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

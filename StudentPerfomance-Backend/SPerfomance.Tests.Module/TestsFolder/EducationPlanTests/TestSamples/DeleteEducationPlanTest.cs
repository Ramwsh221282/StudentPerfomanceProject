using SPerfomance.Api.Module.Converters.EducationPlans;
using SPerfomance.Application.EducationPlans.Module.Commands.Delete;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Tests.Module.TestsFolder.EducationPlanTests.TestSamples;

internal sealed class DeleteEducationPlanTest(EducationPlanSchema schema) : IService<EducationPlan>
{
	private readonly EducationPlanSchema _schema = schema;
	public async Task<OperationResult<EducationPlan>> DoOperation()
	{
		IRepository<EducationPlan> repository = RepositoryProvider.CreateEducationPlansRepository();
		EducationPlansRepositoryObject planParameter = EducationPlanSchemaConverter.ToRepositoryObject(_schema);
		IService<EducationPlan> service = new EducationPlanDeletionService
		(
			EducationPlanExpressionsFactory.CreateFindPlan(planParameter),
			repository
		);
		OperationResult<EducationPlan> result = await service.DoOperation();
		OperationResultLogger<OperationResult<EducationPlan>, EducationPlan> logger =
		new(result, "Education Plan Deletion Log");
		logger.ShowInfo();
		if (result.Result != null && !result.IsFailed)
		{
			Console.WriteLine("\n");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Entiy Number: {result.Result.EntityNumber}");
			Console.WriteLine($"Plan Year: {result.Result.Year}");
			Console.WriteLine($"Direction code: {result.Result.Direction.Code}");
			Console.WriteLine($"Direction name: {result.Result.Direction.Name}");
			Console.WriteLine($"Direction type: {result.Result.Direction.Type}");
			Console.WriteLine($"Semesters count: {result.Result.Semesters.Count}");
			Console.WriteLine("\n");
		}
		return result;
	}
}

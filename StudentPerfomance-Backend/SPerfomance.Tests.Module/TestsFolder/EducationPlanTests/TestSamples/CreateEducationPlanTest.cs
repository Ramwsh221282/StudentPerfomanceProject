using SPerfomance.Api.Module.Converters.EducationDirections;
using SPerfomance.Api.Module.Converters.EducationPlans;
using SPerfomance.Application.EducationPlans.Module.Commands.Create;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections.Expressions;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Tests.Module.TestsFolder.EducationPlanTests.TestSamples;

internal sealed class CreateEducationPlanTest(EducationPlanSchema schema) : IService<EducationPlan>
{
	private readonly EducationPlanSchema _schema = schema;
	public async Task<OperationResult<EducationPlan>> DoOperation()
	{
		IRepository<EducationPlan> plans = RepositoryProvider.CreateEducationPlansRepository();
		IRepository<EducationDirection> directions = RepositoryProvider.CreateDirectionsRepository();
		IRepository<Semester> semesters = RepositoryProvider.CreateSemestersRepository();
		EducationPlansRepositoryObject checkDublicateParameter = EducationPlanSchemaConverter.ToRepositoryObject(_schema);
		EducationDirectionsRepositoryObject findDirectionParameter = EducationDirectionSchemaConverter.ToRepositoryObject(_schema.Direction);
		IService<EducationPlan> service = new EducationPlanCreationService
		(
			_schema,
			EducationPlanExpressionsFactory.CreateFindPlan(checkDublicateParameter),
			EducationDirectionExpressionsFactory.FindDirection(findDirectionParameter),
			plans,
			directions,
			semesters
		);
		OperationResult<EducationPlan> result = await service.DoOperation();
		OperationResultLogger<OperationResult<EducationPlan>, EducationPlan> logger =
		new(result, "Education Plan Creation Log");
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

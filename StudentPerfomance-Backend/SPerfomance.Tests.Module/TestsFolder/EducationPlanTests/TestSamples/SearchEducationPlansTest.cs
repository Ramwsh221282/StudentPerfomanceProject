using SPerfomance.Api.Module.Converters.EducationPlans;
using SPerfomance.Application.EducationPlans.Module.Queries.Search;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Tests.Module.TestsFolder.EducationPlanTests.TestSamples;

public class SearchEducationPlansTest(EducationPlanSchema schema) : IService<IReadOnlyCollection<EducationPlan>>
{
	private readonly EducationPlanSchema _schema = schema;
	public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> DoOperation()
	{
		IRepository<EducationPlan> repository = RepositoryProvider.CreateEducationPlansRepository();
		EducationPlansRepositoryObject parameter = EducationPlanSchemaConverter.ToRepositoryObject(_schema);
		IRepositoryExpression<EducationPlan> expression = EducationPlanExpressionsFactory.CreateFilter(parameter);
		IService<IReadOnlyCollection<EducationPlan>> service = new EducationPlanSearchByFilter(repository, expression);
		OperationResult<IReadOnlyCollection<EducationPlan>> result = await service.DoOperation();
		OperationResultLogger<OperationResult<IReadOnlyCollection<EducationPlan>>, IReadOnlyCollection<EducationPlan>> logger =
		new(result, "Education Plan Search Log");
		logger.ShowInfo();
		if (result.Result != null && !result.IsFailed)
		{
			Console.WriteLine($"Searched result count: {result.Result.Count}");
			foreach (var plan in result.Result)
			{
				Console.WriteLine("\n");
				Console.WriteLine($"ID: {plan.Id}");
				Console.WriteLine($"Entiy Number: {plan.EntityNumber}");
				Console.WriteLine($"Plan Year: {plan.Year}");
				Console.WriteLine($"Direction code: {plan.Direction.Code}");
				Console.WriteLine($"Direction name: {plan.Direction.Name}");
				Console.WriteLine($"Direction type: {plan.Direction.Type}");
				Console.WriteLine($"Semesters count: {plan.Semesters.Count}");
				Console.WriteLine("\n");
			}
		}
		return result;
	}
}

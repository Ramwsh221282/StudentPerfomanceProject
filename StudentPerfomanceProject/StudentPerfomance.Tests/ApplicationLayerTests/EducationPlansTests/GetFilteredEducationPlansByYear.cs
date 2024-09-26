using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationPlanRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.Application.Queries.EducationPlans.CollectionFilters;
using StudentPerfomance.Application.Queries.EducationPlans.FilterConstraints;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationPlansTests;

public sealed class GetFilteredEducationPlansByYear(EducationPlanGeneralRequest request) : IService<IReadOnlyCollection<EducationPlan>>
{
	private readonly EducationPlanSchema _plan = request.Plan;
	private readonly IRepository<EducationPlan> _repository = new EducationPlansRepository();
	public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> DoOperation()
	{
		EducationPlanRepositoryParameter planParameter = EducationPlanSchemaConverter.ToRepositoryParameter(_plan);
		IRepositoryExpression<EducationPlan> generalFilter = EducationPlanExpressionsFactory.CreateFilterByYear(planParameter);
		IService<IReadOnlyCollection<EducationPlan>> service = EducationPlanCollectionFilterBuilder.Build
		(
			new FilterConstraint(FilterConstraint.YearOnly),
			_repository,
			generalFilter
		);
		OperationResult<IReadOnlyCollection<EducationPlan>> result = await service.DoOperation();
		OperationResultLogger<OperationResult<IReadOnlyCollection<EducationPlan>>, IReadOnlyCollection<EducationPlan>> logger =
		new(result, "Education plan filter by year test");
		logger.ShowInfo();
		if (result.Result != null && result.Result.Count > 0)
		{
			Console.WriteLine("Education plan filter by year test info:");
			foreach (var plan in result.Result)
			{
				Console.WriteLine($"ID: {plan.Id}");
				Console.WriteLine($"Entity Number: {plan.EntityNumber}");
				Console.WriteLine($"Year: {plan.Year}");
				Console.WriteLine($"Direction name: {plan.Direction.Name}");
				Console.WriteLine($"Direction type: {plan.Direction.Type}");
				Console.WriteLine($"Direction code: {plan.Direction.Code}");
				Console.WriteLine();
			}
		}
		return result;
	}
}

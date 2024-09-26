using StudentPerfomance.Api.Requests.EducationPlanRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.Application.Queries.EducationPlans.FilterConstraints;
using StudentPerfomance.Application.Queries.EducationPlans.CollectionFilters;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.Api.Converters;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationPlansTests;

public sealed class GetFilteredEducationPlansByDirection(EducationPlanGeneralRequest request) : IService<IReadOnlyCollection<EducationPlan>>
{
	private readonly EducationPlanSchema _plan = request.Plan;
	private readonly IRepository<EducationPlan> _repository = new EducationPlansRepository();
	public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> DoOperation()
	{
		EducationDirectionRepositoryParameter directionParameter = EducationDirectionSchemaConverter.ToRepositoryParameter(_plan.Direction);
		IRepositoryExpression<EducationPlan> generalFilter = EducationPlanExpressionsFactory.CreateFilterByDirection(directionParameter);
		IService<IReadOnlyCollection<EducationPlan>> service = EducationPlanCollectionFilterBuilder.Build
		(
			new FilterConstraint(FilterConstraint.DirectionOnly),
			_repository,
			generalFilter
		);
		OperationResult<IReadOnlyCollection<EducationPlan>> result = await service.DoOperation();
		OperationResultLogger<OperationResult<IReadOnlyCollection<EducationPlan>>, IReadOnlyCollection<EducationPlan>> logger =
		new(result, "Education plan filter by direction test");
		logger.ShowInfo();
		if (result.Result != null && result.Result.Count > 0)
		{
			Console.WriteLine("Education plan filter by direction info:");
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

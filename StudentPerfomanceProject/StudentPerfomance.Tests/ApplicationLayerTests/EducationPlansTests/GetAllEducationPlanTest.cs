using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.EducationPlans.All;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationPlansTests;

public sealed class GetAllEducationPlanTest : IService<IReadOnlyCollection<EducationPlan>>
{
	private readonly IRepository<EducationPlan> _repository = new EducationPlansRepository();
	public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> DoOperation()
	{
		IService<IReadOnlyCollection<EducationPlan>> service = new EducationPlansGetAllService(_repository);
		OperationResult<IReadOnlyCollection<EducationPlan>> result = await service.DoOperation();
		OperationResultLogger<OperationResult<IReadOnlyCollection<EducationPlan>>, IReadOnlyCollection<EducationPlan>> logger =
		new(result, "Education plan get all test");
		logger.ShowInfo();
		if (result.Result != null && result.Result.Count > 0)
		{
			Console.WriteLine("Education plan get all test info:");
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

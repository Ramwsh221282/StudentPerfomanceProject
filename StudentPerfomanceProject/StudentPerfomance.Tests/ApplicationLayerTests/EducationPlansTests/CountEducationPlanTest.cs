using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.EducationPlans.Count;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationPlansTests;

public sealed class CountEducationPlanTest : IService<int>
{
	private readonly IRepository<EducationPlan> _repository = new EducationPlansRepository();
	public async Task<OperationResult<int>> DoOperation()
	{
		IService<int> service = new EducationPlansGetCountService(_repository);
		OperationResult<int> result = await service.DoOperation();
		OperationResultLogger<OperationResult<int>, int> logger =
		new(result, "Education Plan Count Test");
		if (!result.IsFailed)
		{
			Console.WriteLine($"Count is: {result.Result}");
		}
		return result;
	}
}

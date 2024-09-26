using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.EducationPlans.ByPage;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationPlansTests;

public sealed class GetPagedEducationPlanTest(int page, int pageSize) : IService<IReadOnlyCollection<EducationPlan>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepository<EducationPlan> _repository = new EducationPlansRepository();
	public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> DoOperation()
	{
		IService<IReadOnlyCollection<EducationPlan>> service = new EducationPlansGetPagedService(_page, _pageSize, _repository);
		OperationResult<IReadOnlyCollection<EducationPlan>> result = await service.DoOperation();
		OperationResultLogger<OperationResult<IReadOnlyCollection<EducationPlan>>, IReadOnlyCollection<EducationPlan>> logger =
		new(result, "Education plan get paged test");
		logger.ShowInfo();
		if (result.Result != null && result.Result.Count > 0)
		{
			Console.WriteLine("Education plan get paged test info:");
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

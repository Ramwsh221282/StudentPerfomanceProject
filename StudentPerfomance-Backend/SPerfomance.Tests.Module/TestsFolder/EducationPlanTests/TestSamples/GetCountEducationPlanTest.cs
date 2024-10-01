using SPerfomance.Application.EducationPlans.Module.Queries.Count;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Tests.Module.TestsFolder.EducationPlanTests.TestSamples;

internal sealed class GetCountEducationPlanTest : IService<int>
{
	public async Task<OperationResult<int>> DoOperation()
	{
		IRepository<EducationPlan> repository = RepositoryProvider.CreateEducationPlansRepository();
		IService<int> service = new EducationPlansGetCountService(repository);
		OperationResult<int> result = await service.DoOperation();
		OperationResultLogger<OperationResult<int>, int> logger = new
		(result, "Education Plan Get Count Log");
		logger.ShowInfo();
		Console.WriteLine($"Education plans count: {result.Result}");
		return result;
	}
}

using SPerfomance.Application.EducationDirections.Module.Queries.Count;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Tests.Module.TestsFolder.EducationDirectionTests.TestSamples;

internal sealed class GetTotalCountDirectionsTest : IService<int>
{
	public async Task<OperationResult<int>> DoOperation()
	{
		IRepository<EducationDirection> repository = RepositoryProvider.CreateDirectionsRepository();
		IService<int> service = new EducationDirectionCountService(repository);
		OperationResult<int> result = await service.DoOperation();
		OperationResultLogger<OperationResult<int>, int> logger =
		new(result, "Education Direction Count Log");
		if (!result.IsFailed)
		{
			Console.WriteLine($"Total count: {result.Result}");
		}
		return result;
	}
}

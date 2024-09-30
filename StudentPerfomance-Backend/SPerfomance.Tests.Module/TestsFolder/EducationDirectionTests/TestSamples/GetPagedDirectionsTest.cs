using SPerfomance.Application.EducationDirections.Module.Queries.ByPage;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Tests.Module.TestsFolder.EducationDirectionTests.TestSamples;

internal sealed class GetPagedDirectionsTest(int page, int pageSize) : IService<IReadOnlyCollection<EducationDirection>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;

	public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> DoOperation()
	{
		IRepository<EducationDirection> repository = RepositoryProvider.CreateDirectionsRepository();
		IService<IReadOnlyCollection<EducationDirection>> service = new EducationDirectionGetByPageService(_page, _pageSize, repository);
		OperationResult<IReadOnlyCollection<EducationDirection>> result = await service.DoOperation();
		OperationResultLogger<OperationResult<IReadOnlyCollection<EducationDirection>>, IReadOnlyCollection<EducationDirection>> logger
		= new(result, "Education Directions Get All Log Info");
		if (result.Result != null && !result.IsFailed)
		{
			Console.WriteLine("Education Directions list: ");
			foreach (var direction in result.Result)
			{
				Console.WriteLine("\n");
				Console.WriteLine($"Direction ID: {direction.Id}");
				Console.WriteLine($"Direction Entity Number: {direction.EntityNumber}");
				Console.WriteLine($"Direction Name: {direction.Name}");
				Console.WriteLine($"Direction Type: {direction.Type}");
				Console.WriteLine($"Direction Code: {direction.Code}");
				Console.WriteLine("\n");
			}
		}
		return result;
	}
}

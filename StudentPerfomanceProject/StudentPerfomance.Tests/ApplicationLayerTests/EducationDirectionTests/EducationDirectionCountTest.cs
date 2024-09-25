using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.EducationDirections.Count;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationDirectionTests;

public sealed class EducationDirectionCountTest : IService<int>
{
	private readonly IRepository<EducationDirection> _repository = new EducationDirectionRepository();
	public async Task<OperationResult<int>> DoOperation()
	{
		IService<int> service = new EducationDirectionCountService(_repository);
		OperationResult<int> result = await service.DoOperation();
		OperationResultLogger<OperationResult<int>, int> logger = new(result, "Education Directions Count Test");
		logger.ShowInfo();
		Console.WriteLine($"Count: {result.Result}");
		return result;
	}
}

using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.EducationDirections.All;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationDirectionTests;

public sealed class GetAllEducationDirectionsTest : IService<IReadOnlyCollection<EducationDirection>>
{
	private readonly IRepository<EducationDirection> _repository = new EducationDirectionRepository();
	public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> DoOperation()
	{
		IService<IReadOnlyCollection<EducationDirection>> service = new EducationDirectionGetAllService(_repository);
		OperationResult<IReadOnlyCollection<EducationDirection>> result = await service.DoOperation();
		OperationResultLogger<OperationResult<IReadOnlyCollection<EducationDirection>>, IReadOnlyCollection<EducationDirection>> logger =
		new(result, "Education directions fetch all service");
		logger.ShowInfo();
		if (result.Result != null && result.Result.Count > 0)
		{
			Console.WriteLine("Fetch all education directions info:");
			foreach (var direction in result.Result)
			{
				Console.WriteLine($"ID: {direction.Id}");
				Console.WriteLine($"Number: {direction.EntityNumber}");
				Console.WriteLine($"Code: {direction.Code.Code}");
				Console.WriteLine($"Name: {direction.Name.Name}");
				Console.WriteLine($"Type: {direction.Type.Type}");
				Console.WriteLine();
			}
		}
		return result;
	}
}

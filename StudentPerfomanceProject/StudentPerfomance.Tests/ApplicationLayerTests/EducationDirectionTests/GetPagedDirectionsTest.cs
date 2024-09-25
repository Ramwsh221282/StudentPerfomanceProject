using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.EducationDirections.ByPage;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationDirectionTests;

public sealed class GetPagedDirectionsTest(int page, int pageSize) : IService<IReadOnlyCollection<EducationDirection>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepository<EducationDirection> _repository = new EducationDirectionRepository();
	public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> DoOperation()
	{
		IService<IReadOnlyCollection<EducationDirection>> service = new EducationDirectionGetByPageService
		(
			_page,
			_pageSize,
			_repository
		);
		OperationResult<IReadOnlyCollection<EducationDirection>> result = await service.DoOperation();
		OperationResultLogger<OperationResult<IReadOnlyCollection<EducationDirection>>, IReadOnlyCollection<EducationDirection>> logger = new
		(result, "Education Direction Pagination Test");
		logger.ShowInfo();
		if (result.Result != null && result.Result.Count > 0)
		{
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

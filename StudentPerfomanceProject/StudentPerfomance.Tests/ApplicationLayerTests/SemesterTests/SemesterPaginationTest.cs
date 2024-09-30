using StudentPerfomance.Api.Requests.SemesterRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.Semesters.GetSemestersByPage;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.SemesterTests;

public sealed class SemesterPaginationTest(int page, int pageSize) : IService<IReadOnlyCollection<Semester>>
{
	private readonly SemesterByPageRequest _request = new SemesterByPageRequest(page, pageSize);
	private readonly IRepository<Semester> _repository = new SemestersRepository();

	public async Task<OperationResult<IReadOnlyCollection<Semester>>> DoOperation()
	{
		IService<IReadOnlyCollection<Semester>> service = new SemestersPaginationService
		(
			_request.Page,
			_request.PageSize,
			_repository
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<IReadOnlyCollection<Semester>>, IReadOnlyCollection<Semester>> logger
		= new(result, "Semester pagination test");
		if (result.Result != null)
		{
			Console.WriteLine("Semester pagination test info: ");
			Console.WriteLine($"Semesters count: {result.Result}");
			foreach (var item in result.Result)
			{
				Console.WriteLine($"ID: {item.Id}");
				Console.WriteLine($"Semester number: {item.Number.Value}");
			}
		}
		return result;
	}
}

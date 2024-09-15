using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.SemesterRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.Queries.Semesters.GetSemestersByFilter;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.SemesterTests;

public sealed class SemesterByFilterTest(int page, int pageSize, SemesterSchema semester, StudentsGroupSchema group)
: IService<IReadOnlyCollection<Semester>>
{
	private readonly SemesterByFilterAndPageRequest _request = new SemesterByFilterAndPageRequest(page, pageSize, semester, group);
	private readonly IRepository<Semester> _repository = new SemestersRepository();

	public async Task<OperationResult<IReadOnlyCollection<Semester>>> DoOperation()
	{
		SemestersRepositoryParameter semesterParam = SemesterSchemaConverter.ToRepositoryParameter(_request.Semester);
		StudentGroupsRepositoryParameter groupParam = StudentsGroupSchemaConverter.ToRepositoryParameter(_request.Group);
		IService<IReadOnlyCollection<Semester>> service = new SemesterFilterService
		(
			_request.Page,
			_request.PageSize,
			SemesterExpressionFactory.CreateFilter(semesterParam, groupParam),
			_repository
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<IReadOnlyCollection<Semester>>, IReadOnlyCollection<Semester>> logger
		= new(result, "Semester paginated filter test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Semester paginated filter results: ");
			Console.WriteLine($"Count: {result.Result.Count}");
			foreach (var item in result.Result)
			{
				Console.WriteLine($"ID: {item.Id}");
				Console.WriteLine($"Semester number: {item.Number.Value}");
				Console.WriteLine($"Semester of group {item.Group.Name.Name}");
			}
		}
		return result;
	}
}

using StudentPerfomance.Api.Requests.DepartmentRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.Departments.GetDepartmentsByPage;
using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.DepartmentTests;

public sealed class DepartmentByPageTest(int page, int pageSize) : IService<IReadOnlyCollection<TeachersDepartment>>
{
	private readonly DepartmentByPageRequest _request = new DepartmentByPageRequest(page, pageSize);
	private readonly IRepository<TeachersDepartment> _repository = new TeacherDepartmentsRepository();

	public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> DoOperation()
	{
		IService<IReadOnlyCollection<TeachersDepartment>> service = new DepartmentsPaginationService
		(
			_request.Page,
			_request.PageSize,
			_repository
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<IReadOnlyCollection<TeachersDepartment>>, IReadOnlyCollection<TeachersDepartment>> logger
		= new(result, "Departments pagination test");
		if (result.Result != null)
		{
			Console.WriteLine("Departments pagination info:");
			Console.WriteLine($"Departments count: {result.Result.Count}");
			foreach (var item in result.Result)
			{
				Console.WriteLine($"ID: {item.Id}");
				Console.WriteLine($"Name: {item.Name}");
			}
		}
		return result;
	}
}

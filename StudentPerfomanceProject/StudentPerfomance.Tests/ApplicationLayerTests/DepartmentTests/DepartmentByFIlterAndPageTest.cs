using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.DepartmentRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.Queries.Departments.GetDepartmentsByFilterAndPage;
using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.DepartmentTests;

public sealed class DepartmentByFIlterAndPageTest(int page, int pageSize, DepartmentSchema schema) : IService<IReadOnlyCollection<TeachersDepartment>>
{
	private readonly DepartmentByFilterAndPageRequest _request = new DepartmentByFilterAndPageRequest(page, pageSize, schema);
	private readonly IRepository<TeachersDepartment> _repository = new TeacherDepartmentsRepository();

	public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> DoOperation()
	{
		TeachersDepartmentRepositoryParameter parameter = DepartmentSchemaConverter.ToRepositoryParameter(_request.Department);
		IService<IReadOnlyCollection<TeachersDepartment>> service = new DepartmentsFilterWithPaginationService
		(
			page,
			pageSize,
			TeacherDepartmentsExpressionFactory.CreateFilterExpression(parameter),
			_repository
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<IReadOnlyCollection<TeachersDepartment>>, IReadOnlyCollection<TeachersDepartment>> logger
		= new(result, "Departments Filter and Pagination Test info:");
		if (result.Result != null)
		{
			Console.WriteLine($"Filtered departments info:");
			Console.WriteLine($"Count: {result.Result.Count}");
			foreach (var item in result.Result)
			{
				Console.WriteLine($"ID: {item.Id}");
				Console.WriteLine($"Name: {item.Name}");
			}
		}
		return result;
	}
}

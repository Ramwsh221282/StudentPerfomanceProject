using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.DepartmentRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.Queries.Departments.GetDepartmentsByFilter;
using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.DepartmentTests;

public sealed class DepartmentByFilterTest(DepartmentSchema schema) : IService<IReadOnlyCollection<TeachersDepartment>>
{
	private readonly DepartmentSearchRequest _request = new DepartmentSearchRequest(schema);
	private readonly IRepository<TeachersDepartment> _repository = new TeacherDepartmentsRepository();

	public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> DoOperation()
	{
		TeachersDepartmentRepositoryParameter parameter = DepartmentSchemaConverter.ToRepositoryParameter(_request.Department);
		IService<IReadOnlyCollection<TeachersDepartment>> service = new DepartmentsFilterService
		(
			TeacherDepartmentsExpressionFactory.CreateFilterExpression(parameter),
			_repository
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<IReadOnlyCollection<TeachersDepartment>>, IReadOnlyCollection<TeachersDepartment>> logger
		= new(result, "Departments Search Test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Search department test info:");
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

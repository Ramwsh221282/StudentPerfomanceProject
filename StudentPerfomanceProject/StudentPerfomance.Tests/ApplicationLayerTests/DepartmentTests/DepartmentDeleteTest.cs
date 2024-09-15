using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.DepartmentRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Departments.DeleteDepartment;
using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.DepartmentTests;

public sealed class DepartmentDeleteTest(DepartmentSchema schema) : IService<TeachersDepartment>
{
	private readonly DepartmentDeleteRequest _request = new DepartmentDeleteRequest(schema);
	private readonly IRepository<TeachersDepartment> _repository = new TeacherDepartmentsRepository();

	public async Task<OperationResult<TeachersDepartment>> DoOperation()
	{
		TeachersDepartmentRepositoryParameter parameter = DepartmentSchemaConverter.ToRepositoryParameter(_request.Department);
		IService<TeachersDepartment> service = new DepartmentDeletionService
		(
			_request.Department,
			_repository,
			TeacherDepartmentsExpressionFactory.CreateHasDepartmentExpression(parameter)
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<TeachersDepartment>, TeachersDepartment> logger = new(result, "Department deletion test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Department Deletion Info:");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Name: {result.Result.Name}");
		}
		return result;
	}
}

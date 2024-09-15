using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.TeacherRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.Queries.Teachers.GetTeachersCountByDepartment;
using StudentPerfomance.DataAccess.Repositories.Teachers;
using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.TeacherTests;

public sealed class TeacherByDepartmentCountTest(DepartmentSchema schema) : IService<int>
{
	private readonly TeacherCountRequest _request = new TeacherCountRequest(schema);
	private readonly IRepository<Teacher> _repository = new TeachersRepository();

	public async Task<OperationResult<int>> DoOperation()
	{
		TeachersDepartmentRepositoryParameter parameter = DepartmentSchemaConverter.ToRepositoryParameter(_request.Department);
		IService<int> service = new TeachersCountByDepartmentService
		(
			TeacherExpressionFactory.CreateByDepartment(parameter),
			_repository
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<int>, int> logger = new(result, "Teachers count by department test");
		logger.ShowInfo();
		Console.WriteLine($"Teachers count: {result.Result}");
		return result;
	}
}

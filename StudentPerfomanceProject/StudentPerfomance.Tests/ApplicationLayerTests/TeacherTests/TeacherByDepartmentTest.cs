using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.TeacherRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.Queries.Teachers.GetTeachersByDepartment;
using StudentPerfomance.DataAccess.Repositories.Teachers;
using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.TeacherTests;

public sealed class TeacherByDepartmentTest(DepartmentSchema schema) : IService<IReadOnlyCollection<Teacher>>
{
	private readonly TeacherByDepartmentRequest _request = new TeacherByDepartmentRequest(schema);
	private readonly IRepository<Teacher> _repository = new TeachersRepository();

	public async Task<OperationResult<IReadOnlyCollection<Teacher>>> DoOperation()
	{
		TeachersDepartmentRepositoryParameter parameter = DepartmentSchemaConverter.ToRepositoryParameter(_request.Department);
		IService<IReadOnlyCollection<Teacher>> service = new TeachersByDepartmentService
		(
			TeacherExpressionFactory.CreateByDepartment(parameter),
			_repository
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<IReadOnlyCollection<Teacher>>, IReadOnlyCollection<Teacher>> logger
		= new(result, "Teacher by department search test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Teacher by departments searching results:");
			Console.WriteLine($"Searched teachers: {result.Result.Count}");
			foreach (var item in result.Result)
			{
				Console.WriteLine("Teacher info: ");
				Console.WriteLine($"ID: {item.Id}");
				Console.WriteLine($"Name: {item.Name.Name}");
				Console.WriteLine($"Surname: {item.Name.Surname}");
				Console.WriteLine($"Thirdname: {item.Name.Thirdname}");
				Console.WriteLine($"Department: {item.Department.Name}");
			}
		}
		return result;
	}
}

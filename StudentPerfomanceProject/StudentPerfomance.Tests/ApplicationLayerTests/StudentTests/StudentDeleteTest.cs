using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.StudentRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Student.DeleteStudent;
using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.DataAccess.Repositories.Students;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentTests;

public sealed class StudentDeleteTest(StudentSchema schema) : IService<Student>
{
	private readonly StudentDeleteRequest _request = new StudentDeleteRequest(schema);
	private readonly IRepository<Student> _repository = new StudentsRepository();

	public async Task<OperationResult<Student>> DoOperation()
	{
		StudentsRepositoryParameter parameter = StudentSchemaConverter.ToRepositoryParameter(_request.Student);
		IService<Student> service = new StudentDeletionService
		(
			_request.Student,
			_repository,
			StudentsRepositoryExpressionFactory.CreateHasStudentExpression(parameter)
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<Student>, Student> logger = new(result, "Student Deletion Test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Deleted studenf info:");
			Console.WriteLine($"Student ID: {result.Result.Id}");
			Console.WriteLine($"Student Name: {result.Result.Name.Name}");
			Console.WriteLine($"Student Surname: {result.Result.Name.Surname}");
			Console.WriteLine($"Student Thirdname: {result.Result.Name.Thirdname}");
			Console.WriteLine($"Student State: {result.Result.State.State}");
			Console.WriteLine($"Student Recordbook: {result.Result.Recordbook.Recordbook}");
			Console.WriteLine($"Student Group: {result.Result.Group.Name.Name}");
		}
		return result;
	}
}

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.StudentRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Student.CreateStudent;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.DataAccess.Repositories.Students;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentTests;

public sealed class StudentCreateTest(StudentSchema student, StudentsGroupSchema group) : IService<Student>
{
	private readonly StudentCreateRequest _request = new StudentCreateRequest(student, group);
	private readonly IRepository<StudentGroup> _groups = new StudentGroupsRepository();
	private readonly IRepository<Student> _students = new StudentsRepository();

	public async Task<OperationResult<Student>> DoOperation()
	{
		StudentGroupsRepositoryParameter groupParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(_request.Group);
		StudentsRepositoryParameter studentParameter = StudentSchemaConverter.ToRepositoryParameter(_request.Student);
		IService<Student> service = new StudentCreationService
		(
			_request.Student,
			_request.Group,
			_students,
			_groups,
			StudentGroupsExpressionFactory.CreateHasGroupExpression(groupParameter),
			StudentsRepositoryExpressionFactory.CreateHasStudentExpression(studentParameter),
			StudentsRepositoryExpressionFactory.CreateHasRecordbookExpression(studentParameter)
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<Student>, Student> logger = new(result, "Student creation test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Student created");
			Console.WriteLine("Student info:");
			Console.WriteLine($"Student ID: {result.Result.Id}");
			Console.WriteLine($"Student Name: {result.Result.Name.Name}");
			Console.WriteLine($"Student Surname: {result.Result.Name.Surname}");
			Console.WriteLine($"Student Thirdname: {result.Result.Name.Thirdname}");
			Console.WriteLine($"Student Recordbook: {result.Result.Recordbook.Recordbook}");
			Console.WriteLine($"Student State: {result.Result.State.State}");
			Console.WriteLine($"Student Group: {result.Result.Group.Name.Name}");
		}
		return result;
	}
}

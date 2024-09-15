using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.StudentRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Student.ChangeStudentData;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.DataAccess.Repositories.Students;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentTests;

public sealed class StudentUpdateTest(StudentSchema oldSchema, StudentSchema newSchema, StudentsGroupSchema group) : IService<Student>
{
	private readonly StudentUpdateRequest _request = new StudentUpdateRequest(oldSchema, newSchema, group);
	private readonly IRepository<Student> _students = new StudentsRepository();
	private readonly IRepository<StudentGroup> _groups = new StudentGroupsRepository();

	public async Task<OperationResult<Student>> DoOperation()
	{
		StudentsRepositoryParameter oldParameter = StudentSchemaConverter.ToRepositoryParameter(_request.OldData);
		StudentsRepositoryParameter newParameter = StudentSchemaConverter.ToRepositoryParameter(_request.NewData);
		StudentGroupsRepositoryParameter groupParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(_request.Group);
		IService<Student> service = new StudentUpdateService
		(
			_request.NewData,
			_request.Group,
			_students,
			_groups,
			StudentsRepositoryExpressionFactory.CreateHasStudentExpression(oldParameter),
			StudentsRepositoryExpressionFactory.CreateHasRecordbookExpression(newParameter),
			StudentGroupsExpressionFactory.CreateHasGroupExpression(groupParameter)
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<Student>, Student> logger = new(result, "Student Update test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("New student info:");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Name: {result.Result.Name.Name}");
			Console.WriteLine($"Surname: {result.Result.Name.Surname}");
			Console.WriteLine($"Thirdname: {result.Result.Name.Thirdname}");
			Console.WriteLine($"State: {result.Result.State.State}");
			Console.WriteLine($"Recordbook: {result.Result.Recordbook.Recordbook}");
			Console.WriteLine($"Group: {result.Result.Group.Name.Name}");
		}
		return result;
	}
}

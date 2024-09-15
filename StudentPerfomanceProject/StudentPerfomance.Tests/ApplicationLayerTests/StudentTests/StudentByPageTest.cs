using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.StudentRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.Queries.Student.GetStudentsByPage;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.DataAccess.Repositories.Students;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentTests;

public sealed class StudentByPageTest(int page, int pageSize, StudentsGroupSchema group) : IService<IReadOnlyCollection<Student>>
{
	private readonly StudentByPageAndGroupRequest _request = new StudentByPageAndGroupRequest(page, pageSize, group);
	private readonly IRepository<Student> _repository = new StudentsRepository();

	public async Task<OperationResult<IReadOnlyCollection<Student>>> DoOperation()
	{
		StudentGroupsRepositoryParameter parameter = StudentsGroupSchemaConverter.ToRepositoryParameter(_request.Group);
		IService<IReadOnlyCollection<Student>> service = new StudentsPaginationService
		(
			page,
			pageSize,
			_request.Group,
			_repository,
			StudentsRepositoryExpressionFactory.CreateByGroupExpression(parameter)
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<IReadOnlyCollection<Student>>, IReadOnlyCollection<Student>> logger
		= new(result, "Students by page and group");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Students by page and group info:");
			Console.WriteLine($"Count: {result.Result.Count}");
			foreach (var student in result.Result)
			{
				Console.WriteLine("Student info:");
				Console.WriteLine($"ID: {student.Id}");
				Console.WriteLine($"Name: {student.Name.Name}");
				Console.WriteLine($"Surname: {student.Name.Surname}");
				Console.WriteLine($"Thirdname: {student.Name.Thirdname}");
				Console.WriteLine($"State: {student.State.State}");
				Console.WriteLine($"RecordBook: {student.Recordbook.Recordbook}");
				Console.WriteLine($"Group: {student.Group.Name.Name}");
			}
		}
		return result;
	}
}

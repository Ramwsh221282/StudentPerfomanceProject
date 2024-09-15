using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.StudentRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.Application.Queries.Student.GetStudentsByFilter;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.DataAccess.Repositories.Students;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentTests;

public sealed class StudentByFilterTest(int page, int pageSize, StudentSchema student, StudentsGroupSchema group)
: IService<IReadOnlyCollection<Student>>
{
	private readonly StudentFilterAndPageRequest _request = new StudentFilterAndPageRequest(page, pageSize, student, group);
	private readonly IRepository<Student> _repository = new StudentsRepository();

	public async Task<OperationResult<IReadOnlyCollection<Student>>> DoOperation()
	{
		StudentsRepositoryParameter studentParameter = StudentSchemaConverter.ToRepositoryParameter(_request.Student);
		StudentGroupsRepositoryParameter groupsParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(_request.Group);
		IService<IReadOnlyCollection<Student>> service = new StudentsFilterService
		(
			_request.Page,
			_request.PageSize,
			_repository,
			StudentsRepositoryExpressionFactory.CreateFilterExpression(studentParameter, groupsParameter)
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<IReadOnlyCollection<Student>>, IReadOnlyCollection<Student>> logger
		= new(result, "Student filter and pagination test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Student filter and pagination test info:");
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

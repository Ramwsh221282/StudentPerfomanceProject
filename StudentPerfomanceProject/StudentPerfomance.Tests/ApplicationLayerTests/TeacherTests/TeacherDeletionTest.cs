using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.TeacherRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Teachers.DeleteTeacher;
using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.DataAccess.Repositories.Teachers;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.TeacherTests;

public sealed class TeacherDeletionTest(TeacherSchema schema) : IService<Teacher>
{
	private readonly TeacherDeleteRequest _request = new TeacherDeleteRequest(schema);
	private readonly IRepository<Teacher> _repository = new TeachersRepository();
	public async Task<OperationResult<Teacher>> DoOperation()
	{
		TeacherRepositoryParameter parameter = TeacherSchemaConverter.ToRepositoryParameter(_request.Teacher);
		IService<Teacher> service = new TeachersDeletionService
		(
			_request.Teacher,
			_repository,
			TeacherExpressionFactory.CreateByName(parameter)
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<Teacher>, Teacher> logger = new(result, "Teacher deletion test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Teacher deletion info:");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Name: {result.Result.Name.Name}");
			Console.WriteLine($"Surname: {result.Result.Name.Surname}");
			Console.WriteLine($"Thirdname: {result.Result.Name.Thirdname}");
			Console.WriteLine($"Department: {result.Result.Department.Name}");
		}
		return result;
	}
}

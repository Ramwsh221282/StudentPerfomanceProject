using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.TeacherRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Teachers.ChangeTeacherData;
using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.DataAccess.Repositories.Teachers;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.TeacherTests;

public sealed class TeacherUpdateTest(TeacherSchema oldSchema, TeacherSchema newSchema) : IService<Teacher>
{
	private readonly TeacherUpdateRequest _request = new TeacherUpdateRequest(oldSchema, newSchema);
	private readonly IRepository<Teacher> _repository = new TeachersRepository();

	public async Task<OperationResult<Teacher>> DoOperation()
	{
		TeacherRepositoryParameter oldParameter = TeacherSchemaConverter.ToRepositoryParameter(_request.OldTeacher);
		TeacherRepositoryParameter newParameter = TeacherSchemaConverter.ToRepositoryParameter(_request.NewTeacher);
		IService<Teacher> service = new TeacherDataChangeService
		(
			_request.NewTeacher,
			TeacherExpressionFactory.CreateByName(oldParameter),
			TeacherExpressionFactory.CreateByName(newParameter),
			_repository
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<Teacher>, Teacher> logger = new(result, "Teacher name change test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Teacher name changing info:");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Name: {result.Result.Name.Name}");
			Console.WriteLine($"Surname: {result.Result.Name.Surname}");
			Console.WriteLine($"Thirdname: {result.Result.Name.Thirdname}");
			Console.WriteLine($"Department: {result.Result.Department.Name}");
		}
		return result;
	}
}

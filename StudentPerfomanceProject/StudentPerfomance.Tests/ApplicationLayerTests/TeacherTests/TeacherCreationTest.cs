using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.TeacherRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Teachers.CreateTeacher;
using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.DataAccess.Repositories.Teachers;
using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.TeacherTests;

public sealed class TeacherCreationTest(TeacherSchema teacher, DepartmentSchema department) : IService<Teacher>
{
	private readonly TeacherCreateRequest _request = new TeacherCreateRequest(teacher, department);
	private readonly IRepository<Teacher> _teachers = new TeachersRepository();
	private readonly IRepository<TeachersDepartment> _departments = new TeacherDepartmentsRepository();
	public async Task<OperationResult<Teacher>> DoOperation()
	{
		TeacherRepositoryParameter teacherParameter = TeacherSchemaConverter.ToRepositoryParameter(_request.Teacher);
		TeachersDepartmentRepositoryParameter departmentParametr = DepartmentSchemaConverter.ToRepositoryParameter(_request.Department);
		IService<Teacher> service = new TeacherCreationService
		(
			_request.Teacher,
			TeacherExpressionFactory.CreateHasTeacher(teacherParameter, departmentParametr),
			TeacherDepartmentsExpressionFactory.CreateHasDepartmentExpression(departmentParametr),
			_teachers,
			_departments
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<Teacher>, Teacher> logger = new(result, "Teacher creation test info:");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Created teacher info");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Name: {result.Result.Name.Name}");
			Console.WriteLine($"Surname: {result.Result.Name.Surname}");
			Console.WriteLine($"Thirdname: {result.Result.Name.Thirdname}");
			Console.WriteLine($"Department: {result.Result.Department.Name}");
		}
		return result;
	}
}

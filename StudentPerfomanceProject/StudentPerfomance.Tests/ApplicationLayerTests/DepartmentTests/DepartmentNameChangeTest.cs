using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.DepartmentRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Departments.ChangeDepartmentName;
using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.DepartmentTests;

public sealed class DepartmentNameChangeTest(DepartmentSchema oldSchema, DepartmentSchema newSchema) : IService<TeachersDepartment>
{
	private readonly DepartmentUpdateRequest _request = new DepartmentUpdateRequest(oldSchema, newSchema);
	private readonly IRepository<TeachersDepartment> _repository = new TeacherDepartmentsRepository();
	public async Task<OperationResult<TeachersDepartment>> DoOperation()
	{
		TeachersDepartmentRepositoryParameter existance = DepartmentSchemaConverter.ToRepositoryParameter(_request.OldDepartment);
		TeachersDepartmentRepositoryParameter dublicate = DepartmentSchemaConverter.ToRepositoryParameter(_request.NewDepartment);
		IService<TeachersDepartment> service = new DepartmentChangeNameService
		(
			_request.NewDepartment,
			_repository,
			TeacherDepartmentsExpressionFactory.CreateHasDepartmentExpression(existance),
			TeacherDepartmentsExpressionFactory.CreateHasDepartmentExpression(dublicate)
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<TeachersDepartment>, TeachersDepartment> logger = new(result, "Department name change test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Department name change info:");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Name: {result.Result.Name}");
		}
		return result;
	}
}

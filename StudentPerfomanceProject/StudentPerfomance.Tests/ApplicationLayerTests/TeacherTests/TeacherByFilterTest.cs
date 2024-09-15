using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.TeacherRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.Application.Queries.Teachers.GetTeachersByFilter;
using StudentPerfomance.DataAccess.Repositories.Teachers;
using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.TeacherTests;

public sealed class TeacherByFilterTest(int page, int pageSize, TeacherSchema teacher, DepartmentSchema department)
: IService<IReadOnlyCollection<Teacher>>
{
	private readonly TeacherByFilterAndPageRequest _request = new TeacherByFilterAndPageRequest(page, pageSize, teacher, department);
	private readonly IRepository<Teacher> _repository = new TeachersRepository();

	public async Task<OperationResult<IReadOnlyCollection<Teacher>>> DoOperation()
	{
		TeacherRepositoryParameter teacherParam = TeacherSchemaConverter.ToRepositoryParameter(_request.Teacher);
		TeachersDepartmentRepositoryParameter departmentParam = DepartmentSchemaConverter.ToRepositoryParameter(_request.Department);
		IService<IReadOnlyCollection<Teacher>> service = new TeachersFilterService
		(
			_request.Page,
			_request.PageSize,
			TeacherExpressionFactory.CreateFilter(teacherParam, departmentParam),
			_repository
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<IReadOnlyCollection<Teacher>>, IReadOnlyCollection<Teacher>> logger
		= new(result, "Teacher by filter and pagination test");
		if (result.Result != null)
		{
			Console.WriteLine("Teacher by filter and pagination request info:");
			Console.WriteLine($"Count: {result.Result.Count}");
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

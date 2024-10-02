using SPerfomance.Api.Module.Converters.TeacherDepartments;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.TeacherDepartments.Module.Queries.Filtered;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Tests.Module.TestsFolder.TeacherDepartmentsTests.TestSamples;

internal sealed class FilterTeacherDepartmentsTest(DepartmentSchema schema, int page, int pageSize) : IService<IReadOnlyCollection<TeachersDepartment>>
{
	private readonly DepartmentSchema _department = schema;
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> DoOperation()
	{
		DepartmentRepositoryObject parameter = _department.ToRepositoryObject();
		IRepository<TeachersDepartment> repository = RepositoryProvider.CreateDepartmentsRepository();
		IRepositoryExpression<TeachersDepartment> expression = TeachersDepartmentsExpressionsFactory.Filter(parameter);
		IService<IReadOnlyCollection<TeachersDepartment>> service = new TeachersDepartmentFilterService(_page, _pageSize, expression, repository);
		OperationResult<IReadOnlyCollection<TeachersDepartment>> result = await service.DoOperation();
		OperationResultLogger<OperationResult<IReadOnlyCollection<TeachersDepartment>>, IReadOnlyCollection<TeachersDepartment>> logger =
		new(result, "Teachers department filter log info:");
		logger.ShowInfo();
		if (result.Result != null && !result.IsFailed)
		{
			Console.WriteLine($"Filtered Departments Count: {result.Result.Count}");
			foreach (var department in result.Result)
			{
				Console.WriteLine("\n");
				Console.WriteLine($"ID: {department.Id}");
				Console.WriteLine($"Entity number: {department.EntityNumber}");
				Console.WriteLine($"Department Full Name: {department.FullName}");
				Console.WriteLine($"Department Short Name: {department.ShortName}");
				Console.WriteLine($"Teachers count: {department.Teachers.Count}");
				Console.WriteLine("\n");
			}
		}
		return result;
	}
}

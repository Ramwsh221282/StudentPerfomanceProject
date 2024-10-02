using SPerfomance.Application.TeacherDepartments.Module.Queries.Paged;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Tests.Module.TestsFolder.TeacherDepartmentsTests.TestSamples;

internal sealed class DepartmentPaginationTest(int page, int pageSize) : IService<IReadOnlyCollection<TeachersDepartment>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> DoOperation()
	{
		IRepository<TeachersDepartment> repository = RepositoryProvider.CreateDepartmentsRepository();
		IService<IReadOnlyCollection<TeachersDepartment>> service = new TeachersDepartmentPaginationService(_page, _pageSize, repository);
		OperationResult<IReadOnlyCollection<TeachersDepartment>> result = await service.DoOperation();
		OperationResultLogger<OperationResult<IReadOnlyCollection<TeachersDepartment>>, IReadOnlyCollection<TeachersDepartment>> logger
		= new(result, "Teachers department pagination log info");
		logger.ShowInfo();
		if (result.Result != null && !result.IsFailed)
		{
			Console.WriteLine($"Departments paged count: {result.Result.Count}");
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

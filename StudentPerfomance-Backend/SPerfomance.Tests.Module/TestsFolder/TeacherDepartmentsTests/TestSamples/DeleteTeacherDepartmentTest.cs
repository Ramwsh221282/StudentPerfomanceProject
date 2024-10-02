using SPerfomance.Api.Module.Converters.TeacherDepartments;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.TeacherDepartments.Module.Commands.Delete;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Tests.Module.TestsFolder.TeacherDepartmentsTests.TestSamples;

internal sealed class DeleteTeacherDepartmentTest(DepartmentSchema schema) : IService<TeachersDepartment>
{
	private readonly DepartmentSchema _schema = schema;
	public async Task<OperationResult<TeachersDepartment>> DoOperation()
	{
		DepartmentRepositoryObject parameter = _schema.ToRepositoryObject();
		IRepositoryExpression<TeachersDepartment> expression = TeachersDepartmentsExpressionsFactory.HasDepartment(parameter);
		IRepository<TeachersDepartment> repository = RepositoryProvider.CreateDepartmentsRepository();
		IService<TeachersDepartment> service = new TeacherDepartmentDeletionService
		(
			expression,
			repository
		);
		OperationResult<TeachersDepartment> result = await service.DoOperation();
		OperationResultLogger<OperationResult<TeachersDepartment>, TeachersDepartment> logger =
		new(result, "Department Deletion Log Info:");
		logger.ShowInfo();
		if (result.Result != null && !result.IsFailed)
		{
			Console.WriteLine("\n");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Entity number: {result.Result.EntityNumber}");
			Console.WriteLine($"Department Full Name: {result.Result.FullName}");
			Console.WriteLine($"Department Short Name: {result.Result.ShortName}");
			Console.WriteLine($"Teachers count: {result.Result.Teachers.Count}");
			Console.WriteLine("\n");
		}
		return result;
	}
}

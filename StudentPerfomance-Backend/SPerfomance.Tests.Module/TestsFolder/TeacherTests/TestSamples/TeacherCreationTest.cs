using SPerfomance.Api.Module.Converters.TeacherDepartments;
using SPerfomance.Api.Module.Converters.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Commands.Create;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers.Expressions;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Tests.Module.TestsFolder.TeacherTests.TestSamples;

internal sealed class TeacherCreationTest(TeacherSchema teacher) : IService<Teacher>
{
	private readonly TeacherSchema _teacher = teacher;
	public async Task<OperationResult<Teacher>> DoOperation()
	{
		TeacherRepositoryObject teacherParameter = _teacher.ToRepositoryObject();
		DepartmentRepositoryObject departmentParameter = _teacher.Department.ToRepositoryObject();
		IRepositoryExpression<Teacher> dublicateCheck = TeachersRepositoryExpressionFactory.CreateHasTeacher(teacherParameter);
		IRepositoryExpression<TeachersDepartment> findDepartment = TeachersDepartmentsExpressionsFactory.FindByName(departmentParameter);
		IRepository<Teacher> teachers = RepositoryProvider.CreateTeachersRepository();
		IRepository<TeachersDepartment> departments = RepositoryProvider.CreateDepartmentsRepository();
		TeacherCreateCommand command = new TeacherCreateCommand(_teacher, dublicateCheck, findDepartment);
		TeacherCreateCommandHandler handler = new TeacherCreateCommandHandler(teachers, departments);
		OperationResult<Teacher> result = await handler.Handle(command);
		OperationResultLogger<OperationResult<Teacher>, Teacher> logger =
		new(result, "Teacher Creation Log Info");
		logger.ShowInfo();
		if (result.Result != null && !result.IsFailed)
		{
			Console.WriteLine("\n");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Entity Number: {result.Result.EntityNumber}");
			Console.WriteLine($"Name: {result.Result.Name}");
			Console.WriteLine($"JobTitle: {result.Result.JobTitle}");
			Console.WriteLine($"Working Condition: {result.Result.Condition}");
			Console.WriteLine($"Department: {result.Result.Department.FullName}");
			Console.WriteLine("\n");
		}
		return result;
	}
}

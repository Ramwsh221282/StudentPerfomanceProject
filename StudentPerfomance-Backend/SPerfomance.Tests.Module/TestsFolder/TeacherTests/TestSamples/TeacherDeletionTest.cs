using SPerfomance.Api.Module.Converters.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Commands.Delete;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Tests.Module.TestsFolder.TeacherTests.TestSamples;

internal sealed class TeacherDeletionTest(TeacherSchema schema) : IService<Teacher>
{
	private readonly TeacherSchema _schema = schema;

	public async Task<OperationResult<Teacher>> DoOperation()
	{
		TeacherRepositoryObject parameter = _schema.ToRepositoryObject();
		IRepositoryExpression<Teacher> expression = TeachersRepositoryExpressionFactory.CreateHasTeacher(parameter);
		IRepository<Teacher> repository = RepositoryProvider.CreateTeachersRepository();
		TeacherDeleteCommand command = new TeacherDeleteCommand(expression);
		TeacherDeleteCommandHandler handler = new TeacherDeleteCommandHandler(repository);
		OperationResult<Teacher> result = await handler.Handle(command);
		OperationResultLogger<OperationResult<Teacher>, Teacher> logger =
		new(result, "Teacher Deletion Log Info");
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

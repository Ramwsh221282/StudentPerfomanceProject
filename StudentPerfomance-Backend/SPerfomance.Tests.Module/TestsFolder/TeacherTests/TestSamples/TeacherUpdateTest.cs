using System;
using SPerfomance.Api.Module.Converters.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Commands.Update;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Tests.Module.TestsFolder.TeacherTests.TestSamples;

internal sealed class TeacherUpdateTest(TeacherSchema oldSchema, TeacherSchema newSchema) : IService<Teacher>
{
	private readonly TeacherSchema _oldSchema = oldSchema;
	private readonly TeacherSchema _newSchema = newSchema;
	public async Task<OperationResult<Teacher>> DoOperation()
	{
		TeacherRepositoryObject initialTeacher = _oldSchema.ToRepositoryObject();
		TeacherRepositoryObject dublicateTeacher = _newSchema.ToRepositoryObject();
		IRepository<Teacher> repository = RepositoryProvider.CreateTeachersRepository();
		IRepositoryExpression<Teacher> findInitial = TeachersRepositoryExpressionFactory.CreateHasTeacher(initialTeacher);
		IRepositoryExpression<Teacher> findDublicate = TeachersRepositoryExpressionFactory.CreateHasTeacher(dublicateTeacher);
		TeacherUpdateCommand command = new TeacherUpdateCommand(findInitial, findDublicate, _newSchema);
		ICommandHandler<TeacherUpdateCommand, OperationResult<Teacher>> handler = new TeacherUpdateDefaultHandler(repository);
		handler = new TeacherUpdateNameHandler(handler);
		handler = new TeacherUpdateConditionHandler(handler);
		handler = new TeacherUpdateJobTitleHandler(handler);
		OperationResult<Teacher> result = await handler.Handle(command);
		OperationResultLogger<OperationResult<Teacher>, Teacher> logger =
		new(result, "Teacher Update Log Info");
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
		await repository.Commit();
		return result;
	}
}

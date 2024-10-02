using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.Teachers;
using SPerfomance.Api.Module.Responses.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Commands.Update;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Api.Module.Facades.Teachers;

internal sealed class TeacherUpdateFacade(TeacherSchema oldSchema, TeacherSchema newSchema) : IFacade<TeacherResponse>
{
	private readonly TeacherSchema _oldSchema = oldSchema;
	private readonly TeacherSchema _newSchema = newSchema;
	public async Task<ActionResult<TeacherResponse>> Process()
	{
		TeacherRepositoryObject oldParameter = _oldSchema.ToRepositoryObject();
		TeacherRepositoryObject newParameter = _newSchema.ToRepositoryObject();
		IRepositoryExpression<Teacher> findInitial = TeachersRepositoryExpressionFactory.CreateHasTeacher(oldParameter);
		IRepositoryExpression<Teacher> findDublicate = TeachersRepositoryExpressionFactory.CreateHasTeacher(newParameter);
		IRepository<Teacher> repository = RepositoryProvider.CreateTeachersRepository();
		TeacherUpdateCommand command = new TeacherUpdateCommand(findInitial, findDublicate, _newSchema);
		ICommandHandler<TeacherUpdateCommand, OperationResult<Teacher>> handler = new TeacherUpdateDefaultHandler(repository);
		handler = new TeacherUpdateNameHandler(handler);
		handler = new TeacherUpdateConditionHandler(handler);
		handler = new TeacherUpdateJobTitleHandler(handler);
		return TeacherResponse.FromResult(await handler.Handle(command));
	}
}

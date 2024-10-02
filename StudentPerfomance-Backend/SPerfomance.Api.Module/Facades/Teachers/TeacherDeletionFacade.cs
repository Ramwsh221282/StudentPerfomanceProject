using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.Teachers;
using SPerfomance.Api.Module.Responses.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Commands.Delete;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Api.Module.Facades.Teachers;

internal sealed class TeacherDeletionFacade(TeacherSchema teacher) : IFacade<TeacherResponse>
{
	private readonly TeacherSchema _teacher = teacher;
	public async Task<ActionResult<TeacherResponse>> Process()
	{
		TeacherRepositoryObject parameter = _teacher.ToRepositoryObject();
		IRepositoryExpression<Teacher> expression = TeachersRepositoryExpressionFactory.CreateHasTeacher(parameter);
		IRepository<Teacher> repository = RepositoryProvider.CreateTeachersRepository();
		TeacherDeleteCommand command = new TeacherDeleteCommand(expression);
		TeacherDeleteCommandHandler handler = new TeacherDeleteCommandHandler(repository);
		return TeacherResponse.FromResult(await handler.Handle(command));
	}
}

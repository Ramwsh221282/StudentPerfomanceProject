using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Module.Converters.StudentGroups;
using SPerfomance.Api.Module.Responses.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Commands.Update;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Api.Module.Facades.StudentGroups;

internal sealed class StudentGroupUpdateFacade(StudentsGroupSchema oldSchema, StudentsGroupSchema newSchema) : IFacade<StudentGroupResponse>
{
	private readonly StudentsGroupSchema _oldSchema = oldSchema;
	private readonly StudentsGroupSchema _newSchema = newSchema;
	public async Task<ActionResult<StudentGroupResponse>> Process()
	{
		StudentGroupsRepositoryObject oldObject = StudentGroupSchemaConverter.ToRepositoryObject(_oldSchema);
		StudentGroupsRepositoryObject newObject = StudentGroupSchemaConverter.ToRepositoryObject(_newSchema);
		IService<StudentGroup> service = new StudentGroupUpdateService
		(
			_newSchema,
			StudentGroupsRepositoryExpressionFactory.CreateHasGroupExpression(oldObject),
			StudentGroupsRepositoryExpressionFactory.CreateFindByNameExpression(newObject),
			RepositoryProvider.CreateStudentGroupsRepository()
		);
		return StudentGroupResponse.FromResult(await service.DoOperation());
	}
}

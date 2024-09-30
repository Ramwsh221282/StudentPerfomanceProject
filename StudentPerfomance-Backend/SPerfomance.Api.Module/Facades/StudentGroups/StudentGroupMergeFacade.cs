using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Module.Converters.StudentGroups;
using SPerfomance.Api.Module.Responses.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Commands.Merge;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Api.Module.Facades.StudentGroups;

internal sealed class StudentGroupMergeFacade(StudentsGroupSchema groupA, StudentsGroupSchema groupB) : IFacade<StudentGroupResponse>
{
	private readonly StudentsGroupSchema _groupA = groupA;
	private readonly StudentsGroupSchema _groupB = groupB;
	public async Task<ActionResult<StudentGroupResponse>> Process()
	{
		StudentGroupsRepositoryObject groupA = StudentGroupSchemaConverter.ToRepositoryObject(_groupA);
		StudentGroupsRepositoryObject groupB = StudentGroupSchemaConverter.ToRepositoryObject(_groupB);
		IService<StudentGroup> service = new StudentGroupsMergeService
		(
			StudentGroupsRepositoryExpressionFactory.CreateFindByNameExpression(groupA),
			StudentGroupsRepositoryExpressionFactory.CreateFindByNameExpression(groupB),
			RepositoryProvider.CreateStudentGroupsRepository()
		);
		return StudentGroupResponse.FromResult(await service.DoOperation());
	}
}

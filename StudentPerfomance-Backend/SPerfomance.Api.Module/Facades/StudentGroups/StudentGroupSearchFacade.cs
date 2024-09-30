using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.StudentGroups;
using SPerfomance.Api.Module.Responses.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Queries.Searched;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Api.Module.Facades.StudentGroups;

internal sealed class StudentGroupSearchFacade(StudentsGroupSchema schema) : IFacade<IReadOnlyCollection<StudentGroupResponse>>
{
	private readonly StudentsGroupSchema _schema = schema;
	public async Task<ActionResult<IReadOnlyCollection<StudentGroupResponse>>> Process()
	{
		StudentGroupsRepositoryObject group = StudentGroupSchemaConverter.ToRepositoryObject(_schema);
		IService<IReadOnlyCollection<StudentGroup>> service = new StudentGroupsSearchService
		(
			RepositoryProvider.CreateStudentGroupsRepository(),
			StudentGroupsRepositoryExpressionFactory.CreateFilterExpression(group)
		);
		return StudentGroupResponse.FromResult(await service.DoOperation());
	}
}

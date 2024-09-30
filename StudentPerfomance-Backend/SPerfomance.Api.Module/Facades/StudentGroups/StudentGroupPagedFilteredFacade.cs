using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.StudentGroups;
using SPerfomance.Api.Module.Responses.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Queries.PagedFiltered;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Api.Module.Facades.StudentGroups;

internal sealed class StudentGroupPagedFilteredFacade(int page, int pageSize, StudentsGroupSchema schema) : IFacade<IReadOnlyCollection<StudentGroupResponse>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly StudentsGroupSchema _schema = schema;
	public async Task<ActionResult<IReadOnlyCollection<StudentGroupResponse>>> Process()
	{
		StudentGroupsRepositoryObject group = StudentGroupSchemaConverter.ToRepositoryObject(_schema);
		IService<IReadOnlyCollection<StudentGroup>> service = new StudentGroupsFilterService
		(
			_page,
			_pageSize,
			StudentGroupsRepositoryExpressionFactory.CreateFilterExpression(group),
			RepositoryProvider.CreateStudentGroupsRepository()
		);
		return StudentGroupResponse.FromResult(await service.DoOperation());
	}
}

using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Responses.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Queries.Paged;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Api.Module.Facades.StudentGroups;

internal sealed class StudentGroupPagedFacade(int page, int pageSize) : IFacade<IReadOnlyCollection<StudentGroupResponse>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public async Task<ActionResult<IReadOnlyCollection<StudentGroupResponse>>> Process()
	{
		IService<IReadOnlyCollection<StudentGroup>> service = new StudentGroupsPaginationService
		(
			_page,
			_pageSize,
			RepositoryProvider.CreateStudentGroupsRepository()
		);
		return StudentGroupResponse.FromResult(await service.DoOperation());
	}
}

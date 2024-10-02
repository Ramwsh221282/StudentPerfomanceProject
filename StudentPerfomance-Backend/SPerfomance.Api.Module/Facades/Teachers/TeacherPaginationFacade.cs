using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Responses.Teachers;
using SPerfomance.Application.Teachers.Module.Queries.GetPaged;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Api.Module.Facades.Teachers;

internal sealed class TeacherPaginationFacade(int page, int pageSize) : IFacade<IReadOnlyCollection<TeacherResponse>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public async Task<ActionResult<IReadOnlyCollection<TeacherResponse>>> Process()
	{
		IRepository<Teacher> repository = RepositoryProvider.CreateTeachersRepository();
		IService<IReadOnlyCollection<Teacher>> service = new GetPagedTeachersService(_page, _pageSize, repository);
		return TeacherResponse.FromResult(await service.DoOperation());
	}
}

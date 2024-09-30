using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Responses.Semesters;
using SPerfomance.Application.Semesters.Module.Queries.GetPaged;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Api.Module.Facades.Semesters;

internal sealed class SemestersPaginationFacade(int page, int pageSize) : IFacade<IReadOnlyCollection<SemesterResponse>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;

	public async Task<ActionResult<IReadOnlyCollection<SemesterResponse>>> Process()
	{
		IRepository<Semester> repository = RepositoryProvider.CreateSemestersRepository();
		IService<IReadOnlyCollection<Semester>> service = new GetSemestersByPageService(_page, _pageSize, repository);
		return SemesterResponse.FromResult(await service.DoOperation());
	}
}

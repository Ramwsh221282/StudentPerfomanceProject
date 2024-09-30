using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Responses.EducationDirections;
using SPerfomance.Application.EducationDirections.Module.Queries.ByPage;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Api.Module.Facades.EducationDirections;

internal sealed class EducationDirectionGetPagedFacade(int page, int pageSize) : IFacade<IReadOnlyCollection<EducationDirectionResponse>>
{
	private readonly IRepository<EducationDirection> _repository = RepositoryProvider.CreateDirectionsRepository();
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> Process()
	{
		IService<IReadOnlyCollection<EducationDirection>> service = new EducationDirectionGetByPageService(_page, _pageSize, _repository);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}

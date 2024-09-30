using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Responses.EducationPlans;
using SPerfomance.Application.EducationPlans.Module.Queries.ByPage;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Api.Module.Facades.EducationPlans;

internal sealed class EducationPlanGetPagedFacade(int page, int pageSize) : IFacade<IReadOnlyCollection<EducationPlanResponse>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> Process()
	{
		IRepository<EducationPlan> repository = RepositoryProvider.CreateEducationPlansRepository();
		IService<IReadOnlyCollection<EducationPlan>> service = new EducationPlansGetPagedService(_page, _pageSize, repository);
		return EducationPlanResponse.FromResult(await service.DoOperation());
	}
}

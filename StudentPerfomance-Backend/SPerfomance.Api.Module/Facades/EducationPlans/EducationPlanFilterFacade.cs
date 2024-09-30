using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.EducationPlans;
using SPerfomance.Api.Module.Responses.EducationPlans;
using SPerfomance.Application.EducationPlans.Module.Queries.PagedFilters;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Api.Module.Facades.EducationPlans;

internal sealed class EducationPlanFilterFacade
(
	EducationPlanSchema plan,
	int page,
	int pageSize
) : IFacade<IReadOnlyCollection<EducationPlanResponse>>
{
	private readonly EducationPlanSchema _plan = plan;
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> Process()
	{
		IRepository<EducationPlan> repository = RepositoryProvider.CreateEducationPlansRepository();
		EducationPlansRepositoryObject planParameter = EducationPlanSchemaConverter.ToRepositoryObject(_plan);
		IRepositoryExpression<EducationPlan> generalFilter = EducationPlanExpressionsFactory.CreateFilter(planParameter);
		IService<IReadOnlyCollection<EducationPlan>> service = new EducationPlanGetPagedByFilter
		(
			_page,
			_pageSize,
			repository,
			generalFilter
		);
		return EducationPlanResponse.FromResult(await service.DoOperation());
	}
}

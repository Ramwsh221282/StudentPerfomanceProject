using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.EducationPlans;
using SPerfomance.Api.Module.Responses.EducationPlans;
using SPerfomance.Application.EducationPlans.Module.Queries.Search;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Api.Module.Facades.EducationPlans;

internal sealed class EducationPlanSearchFacade(EducationPlanSchema plan) : IFacade<IReadOnlyCollection<EducationPlanResponse>>
{
	private readonly EducationPlanSchema _plan = plan;
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> Process()
	{
		IRepository<EducationPlan> repository = RepositoryProvider.CreateEducationPlansRepository();
		EducationPlansRepositoryObject planParameter = EducationPlanSchemaConverter.ToRepositoryObject(_plan);
		IRepositoryExpression<EducationPlan> generalFilter = EducationPlanExpressionsFactory.CreateFilter(planParameter);
		IService<IReadOnlyCollection<EducationPlan>> service = new EducationPlanSearchByFilter
		(
			repository,
			generalFilter
		);
		return EducationPlanResponse.FromResult(await service.DoOperation());
	}
}

using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationPlanRequests;
using StudentPerfomance.Api.Responses.EducationPlans;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.Application.Queries.EducationPlans.CollectionFilters;
using StudentPerfomance.Application.Queries.EducationPlans.FilterConstraints;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationPlansFacade;

internal sealed class EducationPlanSearchFacade(EducationPlanGeneralRequest request) : IFacade<IReadOnlyCollection<EducationPlanResponse>>
{
	private readonly EducationPlanGeneralRequest _request = request;
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> Process()
	{
		EducationPlanSchema plan = _request.Plan;
		IRepository<EducationPlan> repository = new EducationPlansRepository();
		EducationPlanRepositoryParameter planParameter = EducationPlanSchemaConverter.ToRepositoryParameter(plan);
		EducationDirectionRepositoryParameter directionParameter = EducationDirectionSchemaConverter.ToRepositoryParameter(plan.Direction);
		IRepositoryExpression<EducationPlan> generalFilter = EducationPlanExpressionsFactory.CreateFilter(planParameter, directionParameter);
		IService<IReadOnlyCollection<EducationPlan>> service = EducationPlanCollectionFilterBuilder.Build
		(
			new FilterConstraint(FilterConstraint.General),
			repository,
			generalFilter
		);
		return EducationPlanResponse.FromResult(await service.DoOperation());
	}
}

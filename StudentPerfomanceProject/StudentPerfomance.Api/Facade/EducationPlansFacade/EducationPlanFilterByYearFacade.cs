using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationPlanRequests;
using StudentPerfomance.Api.Responses.EducationPlans;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.Application.Queries.EducationPlans.CollectionPagedFilters;
using StudentPerfomance.Application.Queries.EducationPlans.FilterConstraints;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationPlansFacade;

internal sealed class EducationPlanFilterByYearFacade(EducationPlanGeneralRequest request, int page, int pageSize) : IFacade<IReadOnlyCollection<EducationPlanResponse>>
{
	private readonly EducationPlanGeneralRequest _request = request;
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> Process()
	{
		EducationPlanSchema plan = _request.Plan;
		IRepository<EducationPlan> repository = new EducationPlansRepository();
		EducationPlanRepositoryParameter parameter = EducationPlanSchemaConverter.ToRepositoryParameter(plan);
		IRepositoryExpression<EducationPlan> filter = EducationPlanExpressionsFactory.CreateFilterByYear(parameter);
		IService<IReadOnlyCollection<EducationPlan>> service = EducationPlanPagedCollectionFilterBuilder.Build
		(
			_page,
			_pageSize,
			new FilterConstraint(FilterConstraint.YearOnly),
			repository,
			filter
		);
		return EducationPlanResponse.FromResult(await service.DoOperation());
	}
}

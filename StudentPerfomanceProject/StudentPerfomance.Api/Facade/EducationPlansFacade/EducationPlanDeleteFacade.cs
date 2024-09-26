using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationPlanRequests;
using StudentPerfomance.Api.Responses.EducationPlans;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.EducationPlans.Delete;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationPlansFacade;

internal sealed class EducationPlanDeleteFacade(EducationPlanGeneralRequest request) : IFacade<EducationPlanResponse>
{
	private readonly EducationPlanGeneralRequest _request = request;
	public async Task<ActionResult<EducationPlanResponse>> Process()
	{
		EducationPlanSchema plan = _request.Plan;
		IRepository<EducationPlan> repository = new EducationPlansRepository();
		EducationPlanRepositoryParameter planParameter = EducationPlanSchemaConverter.ToRepositoryParameter(plan);
		EducationDirectionRepositoryParameter directionParameter = EducationDirectionSchemaConverter.ToRepositoryParameter(plan.Direction);
		IService<EducationPlan> service = new DeleteEducationPlanService
		(
			EducationPlanExpressionsFactory.CreateFindPlan(planParameter, directionParameter),
			repository
		);
		return EducationPlanResponse.FromResult(await service.DoOperation());
	}
}

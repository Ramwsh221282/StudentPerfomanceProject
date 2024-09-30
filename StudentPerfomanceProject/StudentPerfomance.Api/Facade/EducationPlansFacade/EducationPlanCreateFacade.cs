using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationPlanRequests;
using StudentPerfomance.Api.Responses.EducationPlans;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.EducationPlans.Create;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationPlansFacade;

internal sealed class EducationPlanCreateFacade(EducationPlanGeneralRequest request) : IFacade<EducationPlanResponse>
{
	private readonly EducationPlanGeneralRequest _request = request;
	public async Task<ActionResult<EducationPlanResponse>> Process()
	{
		EducationPlanSchema plan = _request.Plan;
		IRepository<EducationPlan> plans = new EducationPlansRepository();
		IRepository<EducationDirection> directions = new EducationDirectionRepository();
		IRepository<Semester> semesters = new SemestersRepository();
		EducationPlanRepositoryParameter checkDublicateParameter = EducationPlanSchemaConverter.ToRepositoryParameter(_request.Plan);
		EducationDirectionRepositoryParameter findDirectionParameter = EducationDirectionSchemaConverter.ToRepositoryParameter(_request.Plan.Direction);
		IService<EducationPlan> service = new CreateEducationPlanService
		(
			plan,
			EducationPlanExpressionsFactory.CreateFindPlan(checkDublicateParameter, findDirectionParameter),
			EducationDirectionExpressionsFactory.FindDirection(findDirectionParameter),
			plans,
			directions,
			semesters
		);
		return EducationPlanResponse.FromResult(await service.DoOperation());
	}
}

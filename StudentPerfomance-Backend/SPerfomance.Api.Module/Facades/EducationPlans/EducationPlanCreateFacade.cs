using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.EducationDirections;
using SPerfomance.Api.Module.Converters.EducationPlans;
using SPerfomance.Api.Module.Responses.EducationPlans;
using SPerfomance.Application.EducationPlans.Module.Commands.Create;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections.Expressions;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Api.Module.Facades.EducationPlans;

internal sealed class EducationPlanCreateFacade(EducationPlanSchema plan) : IFacade<EducationPlanResponse>
{
	private readonly EducationPlanSchema _plan = plan;
	public async Task<ActionResult<EducationPlanResponse>> Process()
	{
		IRepository<EducationPlan> plans = RepositoryProvider.CreateEducationPlansRepository();
		IRepository<EducationDirection> directions = RepositoryProvider.CreateDirectionsRepository();
		IRepository<Semester> semesters = RepositoryProvider.CreateSemestersRepository();
		EducationPlansRepositoryObject checkDublicateParameter = EducationPlanSchemaConverter.ToRepositoryObject(_plan);
		EducationDirectionsRepositoryObject findDirectionParameter = EducationDirectionSchemaConverter.ToRepositoryObject(_plan.Direction);
		IService<EducationPlan> service = new EducationPlanCreationService
		(
			_plan,
			EducationPlanExpressionsFactory.CreateFindPlan(checkDublicateParameter),
			EducationDirectionExpressionsFactory.FindDirection(findDirectionParameter),
			plans,
			directions,
			semesters
		);
		return EducationPlanResponse.FromResult(await service.DoOperation());
	}
}

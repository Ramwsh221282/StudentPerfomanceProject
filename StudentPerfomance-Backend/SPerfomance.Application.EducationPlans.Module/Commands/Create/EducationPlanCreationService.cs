using SPerfomance.Application.EducationPlans.Module.Commands.Create.Decorators;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Create;

public sealed class EducationPlanCreationService : IService<EducationPlan>
{
	private readonly CreateEducationPlanCommand _command;
	private readonly ICommandHandler<CreateEducationPlanCommand, OperationResult<EducationPlan>> _handler;
	private readonly IRepository<EducationPlan> _plans;

	public EducationPlanCreationService
	(
		EducationPlanSchema plan,
		IRepositoryExpression<EducationPlan> checkDublicate,
		IRepositoryExpression<EducationDirection> findDirection,
		IRepository<EducationPlan> plans,
		IRepository<EducationDirection> direction,
		IRepository<Semester> semesters
	)
	{
		_plans = plans;
		_command = new CreateEducationPlanCommand(plan, checkDublicate, findDirection);
		_handler = new CreateEducationPlanDefaultHandler(plans, direction);
		_handler = new CreateEducationPlanWithSemestersHandler(_handler, semesters);
	}


	public async Task<OperationResult<EducationPlan>> DoOperation()
	{
		OperationResult<EducationPlan> result = await _handler.Handle(_command);
		if (result.Result == null || result.IsFailed) return result;
		await _plans.Create(result.Result);
		return result;
	}
}

using StudentPerfomance.Application.Commands.EducationPlans.Create.Decorators;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationPlans.Create;

public sealed class CreateEducationPlanService : IService<EducationPlan>
{
	private readonly CreateEducationPlanCommand _command;
	private readonly ICommandHandler<CreateEducationPlanCommand, OperationResult<EducationPlan>> _handler;
	private readonly IRepository<EducationPlan> _plans;

	public CreateEducationPlanService
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

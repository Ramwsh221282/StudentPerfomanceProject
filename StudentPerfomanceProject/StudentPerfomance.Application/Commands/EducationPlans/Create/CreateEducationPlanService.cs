using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationPlans.Create;

public sealed class CreateEducationPlanService
(
	EducationPlanSchema plan,
	IRepositoryExpression<EducationPlan> checkDublicate,
	IRepositoryExpression<EducationDirection> findDirection,
	IRepository<EducationPlan> plans,
	IRepository<EducationDirection> direction
)
 : IService<EducationPlan>
{
	private readonly CreateEducationPlanCommand _command = new CreateEducationPlanCommand(plan, checkDublicate, findDirection);
	private readonly CreateEducationPlanCommandHandler _handler = new CreateEducationPlanCommandHandler(plans, direction);
	public async Task<OperationResult<EducationPlan>> DoOperation() => await _handler.Handle(_command);
}

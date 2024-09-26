using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationPlans.Delete;

public sealed class DeleteEducationPlanService
(
	IRepositoryExpression<EducationPlan> expression,
	IRepository<EducationPlan> repository
) : IService<EducationPlan>
{
	private readonly DeleteEducationPlanCommand _command = new DeleteEducationPlanCommand(expression);
	private readonly DeleteEducationPlanCommandHandler _handler = new DeleteEducationPlanCommandHandler(repository);
	public async Task<OperationResult<EducationPlan>> DoOperation() => await _handler.Handle(_command);
}

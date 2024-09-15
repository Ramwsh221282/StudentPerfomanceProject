using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.SemesterPlans.DeleteSemesterPlan;

public class SemesterPlanDeletionService
(
	IRepositoryExpression<SemesterPlan> existance,
	IRepository<SemesterPlan> repository
)
: IService<SemesterPlan>
{
	private readonly DeleteSemesterPlanCommand _command = new DeleteSemesterPlanCommand(existance);
	private readonly DeleteSemesterPlanCommandHandler _handler = new DeleteSemesterPlanCommandHandler(repository);
	public async Task<OperationResult<SemesterPlan>> DoOperation() => await _handler.Handle(_command);
}

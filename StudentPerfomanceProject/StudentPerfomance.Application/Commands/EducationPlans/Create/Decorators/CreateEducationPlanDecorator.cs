using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Application.Commands.EducationPlans.Create.Decorators;

internal class CreateEducationPlanDecorator
(
	ICommandHandler<CreateEducationPlanCommand, OperationResult<EducationPlan>> handler
) : ICommandHandler<CreateEducationPlanCommand, OperationResult<EducationPlan>>
{
	private readonly ICommandHandler<CreateEducationPlanCommand, OperationResult<EducationPlan>> _handler = handler;
	public virtual async Task<OperationResult<EducationPlan>> Handle(CreateEducationPlanCommand command) => await _handler.Handle(command);
}

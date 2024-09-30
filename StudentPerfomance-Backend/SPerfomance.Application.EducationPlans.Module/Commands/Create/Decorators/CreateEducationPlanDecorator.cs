using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Create.Decorators;

internal class CreateEducationPlanDecorator
(
	ICommandHandler<CreateEducationPlanCommand, OperationResult<EducationPlan>> handler
) : ICommandHandler<CreateEducationPlanCommand, OperationResult<EducationPlan>>
{
	private readonly ICommandHandler<CreateEducationPlanCommand, OperationResult<EducationPlan>> _handler = handler;
	public virtual async Task<OperationResult<EducationPlan>> Handle(CreateEducationPlanCommand command) => await _handler.Handle(command);
}

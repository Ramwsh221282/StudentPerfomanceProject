using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Delete;

internal sealed class DeleteEducationPlanCommand(IRepositoryExpression<EducationPlan> expression) : ICommand
{
	public IRepositoryExpression<EducationPlan> Expression { get; init; } = expression;
}

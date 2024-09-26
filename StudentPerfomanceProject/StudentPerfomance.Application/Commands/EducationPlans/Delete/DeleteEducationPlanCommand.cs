using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationPlans.Delete;

internal sealed class DeleteEducationPlanCommand(IRepositoryExpression<EducationPlan> expression) : ICommand
{
	public IRepositoryExpression<EducationPlan> Expression { get; init; } = expression;
}

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.SemesterPlans.DeleteSemesterPlan;

internal sealed class DeleteSemesterPlanCommand(IRepositoryExpression<SemesterPlan> existance) : ICommand
{
	public IRepositoryExpression<SemesterPlan> Existance { get; init; } = existance;
}

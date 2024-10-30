using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Commands.RemoveEducationPlan;

public class RemoveEducationPlanCommand(EducationPlan? plan) : ICommand<EducationPlan>
{
	public EducationPlan? Plan { get; init; } = plan;
}

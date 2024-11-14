using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationPlans.Commands.ChangeEducationPlanYear;

public class ChangeEducationPlanYearCommand(EducationPlan? plan, int? newYear)
    : ICommand<EducationPlan>
{
    public EducationPlan? Plan { get; init; } = plan;

    public int NewYear { get; init; } = newYear.ValueOrEmpty();
}

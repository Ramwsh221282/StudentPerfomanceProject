using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationPlans.Commands.CreateEducationPlan;

public class CreateEducationPlanCommand(EducationDirection? direction, int? year)
    : ICommand<EducationPlan>
{
    public EducationDirection? Direction { get; init; } = direction;

    public int Year { get; init; } = year.ValueOrEmpty();
}

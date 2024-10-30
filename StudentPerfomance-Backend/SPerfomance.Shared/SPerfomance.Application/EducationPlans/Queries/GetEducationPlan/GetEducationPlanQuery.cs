using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;

public class GetEducationPlanQuery(EducationDirection? direction, int? planYear) : IQuery<EducationPlan>
{
	public EducationDirection? Direction { get; init; } = direction;

	public int PlanYear { get; init; } = planYear.ValueOrEmpty();
}

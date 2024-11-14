using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;

public class GetEducationPlanQueryHandler : IQueryHandler<GetEducationPlanQuery, EducationPlan>
{
    public async Task<Result<EducationPlan>> Handle(GetEducationPlanQuery command)
    {
        if (command.Direction == null)
            return Result<EducationPlan>.Failure(EducationDirectionErrors.NotFoundError());

        Result<EducationPlan> plan = command.Direction.GetEducationPlan(command.PlanYear);
        return await Task.FromResult(plan);
    }
}

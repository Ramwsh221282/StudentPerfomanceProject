using SPerfomance.Domain.Models.EducationPlans;

namespace SPerfomance.Api.Features.Users.GetAdminShortInfoFeature.Responses;

public class EducationPlanShortInfoResponse
{
    public Guid Id { get; }
    public int Year { get; }
    public bool HasGroups { get; }
    public List<StudentGroupShortInfoResponse> Groups { get; } = [];
    public List<SemesterShortInfoResponse> Semesters { get; } = [];

    public EducationPlanShortInfoResponse(EducationPlan plan)
    {
        Id = plan.Id;
        Year = plan.Year.Year;
        foreach (var group in plan.Groups)
            Groups.Add(new StudentGroupShortInfoResponse(group));
        foreach (var semester in plan.Semesters)
            Semesters.Add(new SemesterShortInfoResponse(semester));
        HasGroups = Groups.Any();
    }
}

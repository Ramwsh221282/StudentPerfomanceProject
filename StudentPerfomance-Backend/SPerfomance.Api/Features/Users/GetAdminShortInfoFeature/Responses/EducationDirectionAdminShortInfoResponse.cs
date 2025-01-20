using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.Api.Features.Users.GetAdminShortInfoFeature.Responses;

public class EducationDirectionAdminShortInfoResponse
{
    public Guid Id { get; }
    public string Name { get; }
    public string Code { get; }
    public string Type { get; }
    public bool HasPlans { get; }
    public List<EducationPlanShortInfoResponse> Plans { get; } = [];

    public EducationDirectionAdminShortInfoResponse(EducationDirection direction)
    {
        Id = direction.Id;
        Name = direction.Name.Name;
        Code = direction.Code.Code;
        Type = direction.Type.Type;
        foreach (var plan in direction.Plans)
            Plans.Add(new EducationPlanShortInfoResponse(plan));
        Plans = Plans.OrderBy(p => p.Year).ToList();
        HasPlans = Plans.Any();
    }
}

using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.Api.Features.Users.GetAdminShortInfoFeature.Responses;

public sealed class AdminShortInfoResponse
{
    public List<EducationDirectionAdminShortInfoResponse> Directions { get; } = [];

    public AdminShortInfoResponse(IEnumerable<EducationDirection> directions)
    {
        foreach (var direction in directions)
            Directions.Add(new EducationDirectionAdminShortInfoResponse(direction));
    }
}

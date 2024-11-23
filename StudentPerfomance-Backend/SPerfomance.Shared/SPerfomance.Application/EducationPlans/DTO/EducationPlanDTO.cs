using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Domain.Models.EducationPlans;

namespace SPerfomance.Application.EducationPlans.DTO;

public class EducationPlanDto
{
    public Guid Id { get; set; }
    public EducationDirectionDto? Direction { get; set; }
    public int? Year { get; set; }
    public int? EntityNumber { get; set; }
}

public static class EducationPlanDtoExtensions
{
    public static EducationPlanDto MapFromDomain(this EducationPlan plan) =>
        new()
        {
            Id = plan.Id,
            Direction = plan.Direction.MapFromDomain(),
            Year = plan.Year.Year,
            EntityNumber = plan.EntityNumber,
        };
}

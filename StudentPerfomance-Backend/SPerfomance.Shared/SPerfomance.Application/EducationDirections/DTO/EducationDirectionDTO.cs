using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.Application.EducationDirections.DTO;

public sealed class EducationDirectionDto
{
    public Guid Id { get; set; }
    public int? EntityNumber { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Code { get; set; }
    public int? EducationPlansCount { get; set; }
    public List<EducationPlanDto> Plans { get; set; } = [];
}

public static class EducationDirectionDtoExtensions
{
    // public static EducationDirectionDto MapFromDomain(this EducationDirection direction) =>
    //     new()
    //     {
    //         Id = direction.Id,
    //         Name = direction.Name.Name,
    //         Type = direction.Type.Type,
    //         Code = direction.Code.Code,
    //         EntityNumber = direction.EntityNumber,
    //         EducationPlansCount = direction.Plans.Count,
    //     };

    public static EducationDirectionDto MapFromDomain(this EducationDirection direction)
    {
        EducationDirectionDto dto =
            new()
            {
                Id = direction.Id,
                Name = direction.Name.Name,
                Type = direction.Type.Type,
                Code = direction.Code.Code,
                EntityNumber = direction.EntityNumber,
                EducationPlansCount = direction.Plans.Count,
            };
        dto = dto.MapDirectionPlansList(direction);
        return dto;
    }

    public static EducationDirectionDto MapDirectionPlansList(
        this EducationDirectionDto dto,
        EducationDirection direction
    )
    {
        if (direction.Plans.Count == 0)
            return dto;

        dto.Plans = direction.Plans.Select(p => p.MapWithoutDirection()).ToList();
        return dto;
    }
}

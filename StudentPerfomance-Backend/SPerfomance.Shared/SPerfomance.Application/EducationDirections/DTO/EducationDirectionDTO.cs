using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.Application.EducationDirections.DTO;

public sealed class EducationDirectionDto
{
    public Guid Id { get; set; }

    public int? EntityNumber { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Code { get; set; }
}

public static class EducationDirectionDtoExtensions
{
    public static EducationDirectionDto MapFromDomain(this EducationDirection direction) =>
        new()
        {
            Id = direction.Id,
            Name = direction.Name.Name,
            Type = direction.Type.Type,
            Code = direction.Code.Code,
            EntityNumber = direction.EntityNumber,
        };
}

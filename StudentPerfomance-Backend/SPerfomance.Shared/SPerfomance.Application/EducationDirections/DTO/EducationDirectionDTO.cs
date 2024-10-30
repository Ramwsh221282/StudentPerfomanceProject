using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.Application.EducationDirections.DTO;

public sealed class EducationDirectionDTO
{
	public int? EntityNumber { get; set; }

	public string? Name { get; set; }

	public string? Type { get; set; }

	public string? Code { get; set; }
}

public static class EducationDirectionDTOExtensions
{
	public static EducationDirectionDTO MapFromDomain(this EducationDirection direction) =>
		new EducationDirectionDTO()
		{
			Name = direction.Name.Name,
			Type = direction.Type.Type,
			Code = direction.Code.Code,
			EntityNumber = direction.EntityNumber
		};
}

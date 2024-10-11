using System.Text.Json.Serialization;

using SPerfomance.Application.Shared.Module.DTOs.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;

namespace SPerfomance.Application.Shared.Module.DTOs.EducationPlans;

public class EducationPlanDTO
{
	[JsonPropertyName("year")]
	public int Year { get; set; }

	[JsonPropertyName("direction")]
	public EducationDirectionDTO? Direction { get; set; }
}

public static class EducationPlanDTOExtensions
{
	public static EducationPlanSchema ToSchema(this EducationPlanDTO? dto)
	{
		if (dto == null)
			return new EducationPlanSchema(0, null);

		EducationDirectionSchema direction = dto.Direction.ToSchema();
		return new EducationPlanSchema(Convert.ToUInt32(dto.Year), direction);
	}
}

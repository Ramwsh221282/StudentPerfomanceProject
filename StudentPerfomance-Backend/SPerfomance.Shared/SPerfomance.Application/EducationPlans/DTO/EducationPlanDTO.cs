using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Domain.Models.EducationPlans;

namespace SPerfomance.Application.EducationPlans.DTO;

public class EducationPlanDTO
{
	public EducationDirectionDTO? Direction { get; set; }

	public int? Year { get; set; }

	public int? EntityNumber { get; set; }
}

public static class EducationPlanDTOExtensions
{
	public static EducationPlanDTO MapFromDomain(this EducationPlan plan) =>
		new EducationPlanDTO()
		{
			Direction = plan.Direction.MapFromDomain(),
			Year = plan.Year.Year,
			EntityNumber = plan.EntityNumber
		};
}

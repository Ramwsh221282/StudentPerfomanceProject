using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Application.StudentGroups.DTO;

public class StudentGroupDTO
{
	public int? EntityNumber { get; set; }

	public string? Name { get; set; }

	public EducationPlanDTO? Plan { get; set; }
}

public static class StudentGroupDTOExtensions
{
	public static StudentGroupDTO MapFromDomain(this StudentGroup group)
	{
		EducationPlanDTO? planDTO = group.EducationPlan == null ?
			null :
			group.EducationPlan.MapFromDomain();
		return new StudentGroupDTO()
		{
			EntityNumber = group.EntityNumber,
			Name = group.Name.Name,
			Plan = planDTO
		};
	}
}

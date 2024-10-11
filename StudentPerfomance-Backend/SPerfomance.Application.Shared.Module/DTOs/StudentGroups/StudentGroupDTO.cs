using System.Text.Json.Serialization;

using SPerfomance.Application.Shared.Module.DTOs.EducationPlans;
using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;

namespace SPerfomance.Application.Shared.Module.DTOs.StudentGroups;

public class StudentGroupDTO
{
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("educationPlan")]
	public EducationPlanDTO? EducationPlan { get; set; }
}

public static class StudentGroupDTOExtensions
{
	public static StudentsGroupSchema ToSchema(this StudentGroupDTO? dto)
	{
		if (dto == null)
			return new StudentsGroupSchema();

		string name = dto.Name.CreateValueOrEmpty();
		EducationPlanSchema plan = dto.EducationPlan.ToSchema();
		return new StudentsGroupSchema(name, plan);
	}
}

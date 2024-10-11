using System.Text.Json.Serialization;

using SPerfomance.Application.Shared.Module.DTOs.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;

namespace SPerfomance.Application.Shared.Module.DTOs.Semesters;

public class SemesterDTO
{
	[JsonPropertyName("number")]
	public byte Number { get; set; }

	[JsonPropertyName("plan")]
	public EducationPlanDTO? Plan { get; set; }
}

public static class SemesterDTOExtensions
{
	public static SemesterSchema ToSchema(this SemesterDTO? semesterDTO)
	{
		if (semesterDTO == null)
			return new SemesterSchema(0);

		EducationPlanSchema plan = semesterDTO.Plan.ToSchema();
		SemesterSchema semester = new SemesterSchema(semesterDTO.Number);
		semester.SetEducationPlan(plan);
		return semester;
	}
}

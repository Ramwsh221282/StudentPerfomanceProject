using System.Text.Json.Serialization;

using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;

namespace SPerfomance.Application.Shared.Module.DTOs.SemesterPlans;

public class SemesterPlanDTO
{
	[JsonPropertyName("discipline")]
	public string? Discipline { get; set; }
}

public static class SemesterPlanDTOExtensions
{
	public static SemesterPlanSchema ToSchema(this SemesterPlanDTO? dto)
	{
		if (dto == null)
			return new SemesterPlanSchema(string.Empty, null);

		string discipline = dto.Discipline.CreateValueOrEmpty();
		return new SemesterPlanSchema(discipline, null);
	}
}

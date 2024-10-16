using System.Text.Json.Serialization;
using SPerfomance.Application.Shared.Module.DTOs.Semesters;
using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;

namespace SPerfomance.Application.Shared.Module.DTOs.SemesterPlans;

public class SemesterPlanDTO
{
	[JsonPropertyName("semester")]
	public SemesterDTO? Semester { get; set; }

	[JsonPropertyName("discipline")]
	public string? Discipline { get; set; }
}

public static class SemesterPlanDTOExtensions
{
	public static SemesterPlanSchema ToSchema(this SemesterPlanDTO? dto)
	{
		if (dto == null)
			return new SemesterPlanSchema();

		string discipline = dto.Discipline.CreateValueOrEmpty();
		SemesterSchema semester = dto.Semester.ToSchema();
		TeacherSchema teacher = new TeacherSchema();
		return new SemesterPlanSchema(discipline, semester, teacher);
	}
}

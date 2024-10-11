using System.Text.Json.Serialization;
using SPerfomance.Application.Shared.Module.DTOs.SemesterPlans;
using SPerfomance.Application.Shared.Module.DTOs.Semesters;

namespace SPerfomance.Application.SemesterPlans.Module.Api.Requests;

public class CreateSemesterDiscipline
{
	[JsonPropertyName("semester")]
	public SemesterDTO? Semester { get; set; }

	[JsonPropertyName("semesterPlan")]
	public SemesterPlanDTO? SemesterPlan { get; set; }
}
